using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;

namespace Api.WordPress.Database
{
    public class Filebase
    {
        private string _root;
        private string _blogRoot;
        private static Filebase _instance;


        public static Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        private Filebase()
        {
            // store data under the application's base directory so it works across environments
            _root = Path.Combine(AppContext.BaseDirectory, "data");
            _blogRoot = Path.Combine(_root, "Blogs");

            if (!Directory.Exists(_blogRoot))
            {
                Directory.CreateDirectory(_blogRoot);
            }
        }

        public int LastBlogKey
        {
            get
            {
                if (Blogs.Any())
                {
                    return Blogs.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        public Blog AddOrUpdate(Blog blog)
        {
            //set up a new Id if one doesn't already exist
            if(blog.Id <= 0)
            {
                blog.Id = LastBlogKey + 1;
            }

            //go to the right place
            string path = Path.Combine(_blogRoot, $"{blog.Id}.json");

            //ensure directory exists
            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir);

            //write the file (overwrite if exists)
            File.WriteAllText(path, JsonSerializer.Serialize(blog, _jsonOptions));

            //return the item, which now has an id
            return blog;
        }
        
        public List<Blog> Blogs
        {
            get
            {
                var _blogs = new List<Blog>();
                if (!Directory.Exists(_blogRoot)) return _blogs;

                var root = new DirectoryInfo(_blogRoot);
                foreach (var file in root.GetFiles("*.json"))
                {
                    try
                    {
                        var text = File.ReadAllText(file.FullName);
                        var blog = JsonSerializer.Deserialize<Blog>(text, _jsonOptions);
                        if (blog != null) _blogs.Add(blog);
                    }
                    catch
                    {
                        // ignore malformed files
                    }
                }

                return _blogs;
            }
        }


        public bool Delete(string type, string id)
        {
            try
            {
                // currently only support "blog" type; ignore type otherwise
                var dir = _blogRoot;
                var path = Path.Combine(dir, $"{id}.json");
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }


   
}