using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoList.Models
{
	public class Category
	{
		public int Id { get; set; }

		[Display(Name = "Category")]
		public string Name { get; set; }

		public virtual List<TodoItem> TodoItems { get; set; }

		public Category()
		{
			TodoItems = new List<TodoItem>();
		}
	}
}