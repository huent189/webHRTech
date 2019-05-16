using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using test_entity.Models;

namespace test_entity.Repository
{
    class CategoryTree
    {
        public Category root { get; set; } 
        public List<CategoryTree> childrent { get; set; }
        public CategoryTree()
        {
            root = new Category();
            childrent = new List<CategoryTree>();
        }
    }
    class CategoryProcess : CommonProcess
    {
        public CategoryTree buildCategoryTree(List<string> data)
        {
            CategoryTree tree = new CategoryTree();
            tree.root = new Category();
            tree.root.Id = Guid.NewGuid();
            tree.root.Name = "ROOT";
            foreach(var row in data)
            {
                string[] nodes = row.Split('.');
                CategoryTree currentTree = tree;
                for(int i = 1; i < nodes.Length; i++)
                {
                    var child = currentTree.childrent.FirstOrDefault(c => c.root.Name.Equals(nodes[i]));
                    if (child == null)
                    {
                        child = new CategoryTree();
                        child.root.Id = Guid.NewGuid();
                        child.root.Name = nodes[i];
                        child.root.ParentId = currentTree.root.Id;
                        currentTree.childrent.Add(child);
                    }                 
                    currentTree = child;
                }
            }
            return tree;
        }

        public void saveToDB(String filePath, int worksheetNumber)
        {
            List<string> data = readDataFromExcel(filePath, worksheetNumber);
            CategoryTree tree = buildCategoryTree(data);

            using (var sqlBulk = new SqlBulkCopy(_connectionString))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(Guid));
                dt.Columns.Add("Name");
                dt.Columns.Add("ParentId", typeof(Guid));
                Queue<CategoryTree> treeQueue = new Queue<CategoryTree>();
                treeQueue.Enqueue(tree);
                while(treeQueue.Count != 0)
                {
                    CategoryTree tempTree = treeQueue.Dequeue();
                    dt.Rows.Add(tempTree.root.Id, tempTree.root.Name, tempTree.root.ParentId);
                    tempTree.childrent.ForEach(c => treeQueue.Enqueue(c));
                }
                sqlBulk.DestinationTableName = "Category";
                sqlBulk.WriteToServer(dt);
            }
        }
    }
}
