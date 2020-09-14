using System;
using System.Data.SqlClient;

namespace LittleLogBook.Data.Entities.Base
{
    public class BaseEntity
	{
		private bool _isNew = true;
		private bool _isDirty = false;
		private DateTime _dateCreated = DateTime.UtcNow;
		private DateTime? _dateModified = null;
		private DateTime? _dateViewed = null;
		private Guid _createdByUserId = Guid.Empty;
		private Guid? _viewedByUserId = null;
		private Guid? _modifiedByUserId = null;

		public bool IsNew
		{
			get
			{
				return _isNew;
			}
			protected set
			{
				_isNew = value;
			}
		}

		public bool IsDirty
		{
			get
			{
				return _isDirty;
			}
			protected set
			{
				_isDirty = value;
			}
		}

		public Guid CreatedByUserId
		{
			get
			{
				return _createdByUserId;
			}
		}

		public DateTime DateCreated
		{
			get
			{
				return _dateCreated;
			}
		}

		public Guid? ViewedByUserId
		{
			get
			{
				return _viewedByUserId;
			}
			internal set
			{
				_viewedByUserId = value;
			}
		}

		public DateTime? DateViewed
		{
			get
			{
				return _dateViewed;
			}
			internal set
			{
				_dateViewed = value;
			}
		}

		public Guid? ModifiedByUserId
		{
			get
			{
				return _modifiedByUserId;
			}
		}

		public DateTime? DateModified
		{
			get
			{
				return _dateModified;
			}
		}

		public BaseEntity(Guid CreatedByUserId)
		{
			_isDirty = true;
			_createdByUserId = CreatedByUserId;
		}

		public BaseEntity(Guid ViewedByUserId, SqlDataReader Reader)
		{
			string fieldName = null;

			fieldName = "CreatedByUserId";
			_createdByUserId = Reader.GetGuid(Reader.GetOrdinal(fieldName));

			fieldName = "DateCreated";
			_dateCreated = Reader.GetDateTime(Reader.GetOrdinal(fieldName));

			fieldName = "ModifiedByUserId";
			if (!Reader.IsDBNull(Reader.GetOrdinal(fieldName)))
			{
				_modifiedByUserId = Reader.GetGuid(Reader.GetOrdinal(fieldName));
			}

			fieldName = "DateModified";
			if (!Reader.IsDBNull(Reader.GetOrdinal(fieldName)))
			{
				_dateModified = Reader.GetDateTime(Reader.GetOrdinal(fieldName));
			}

			_viewedByUserId = ViewedByUserId;
			_dateViewed = DateTime.UtcNow;
			_isNew = false;
		}

		internal void SetInternals(bool IsNew, bool IsDirty, DateTime? DateModified, Guid? ModifiedByUserId)
		{
			_isNew = IsNew;
			_isDirty = IsDirty;
			_modifiedByUserId = ModifiedByUserId;
			_viewedByUserId = ModifiedByUserId;
			_dateModified = DateModified;
		}
	}
}
