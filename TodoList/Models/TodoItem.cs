using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TodoList.Models
{
	public class TodoItem
	{
		public int Id { get; set; }
		public string Task { get; set; }

		[Display(Name = "Date Completed")]
		public DateTime? DateCompleted { get; set;}

		// Relationships One To Many
		[DisplayName("Category")]
		public virtual int CategoryId { get; set; }
		public virtual Category Category { get; set; }

		public bool IsItemDone()
		{
			// Item done if there is a date completed
			return DateCompleted != null;
		}

		public void MarkItemComplete()
		{
			// mark completed item with date
			DateCompleted = DateTime.Now;
		}

	}
}