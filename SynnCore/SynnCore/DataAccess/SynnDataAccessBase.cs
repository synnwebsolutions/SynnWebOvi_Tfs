using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Data.SqlClient;

namespace SynnCore.DataAccess
{
     public abstract class SynnDataAccessBase : MarshalByRefObject, IDisposable
    {
        bool disposed = false;
        bool ownConnection = true;
        bool _inTransaction = false;
        bool _inGlobalTransaction = false;
        IDbConnection _connection;
        IDbTransaction _trans;
        protected IDbCommand _command;
        int _setFieldCounter = 0;
        int _orderbyFieldCounter = 0;
        int _whereFieldCounter = 0;
        SynnDataProviderBase _dataProvider;
        DataAcessBaseChildList _childList;

        protected void SetInsertIntoSql(string tableName, SqlItemList parameters)
        {
            SetSqlFormat("insert into {0} ({1}) values ({2})", tableName, GetCommaFieldList(parameters.Select(x => x.FieldName).ToList()), GetCommaListOfChar('?', parameters.Count));
            SetParameters(parameters.Select(x => x.FieldValue).ToArray());
        }

        protected string GetCommaListOfChar(char c, int count)
        {
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                if (i != 0 && i < count)
                    b.Append(",");
                b.Append(c);
            }
            return b.ToString();
        }
        protected string GetCommaFieldList(ICollection lst)
        {
            StringBuilder b = new StringBuilder();
            int counter = 0;
            foreach (string s in lst)
            {
                if (counter != 0 && counter < lst.Count)
                    b.Append(",");
                b.Append(s);
                counter++;
            }
            return b.ToString();
        }

        protected virtual DataAcessBaseChildList GetChildList()
        {
            return new DataAcessBaseChildList(this);
        }

        /// <summary>
        /// introduces child list collection, items here get instaced automaticly when call to GetInstance is made,
        /// The owner dispose method will clear the list
        /// The Getinstance method check if a instance of the requested type already exist and returns it otherwise it creates a new instance
        /// </summary>
        protected DataAcessBaseChildList ChildList
        {
            get
            {
                if (_childList == null)
                    _childList = GetChildList();
                return _childList;
            }
        }

        protected internal SynnDataProviderBase DataProvider
        {
            get { return _dataProvider; }
        }

        public SynnDataAccessBase(SynnDataProviderBase d)
        {
            _dataProvider = d;
            _command = d.getCommand();
            _command.CommandType = CommandType.Text;
            SetConnection(d.getConnection());
        }

        void SetConnection(IDbConnection con)
        {
            _connection = con;
            _command.Connection = con;
        }

        void SetTransaction(IDbTransaction t)
        {
            _trans = t;
            _command.Transaction = t;
        }

        /// <summary>
        /// returns true when global transaction is active
        /// </summary>
        public bool InGlobalTransaction
        {
            get { return _inGlobalTransaction; }
            protected set { _inGlobalTransaction = value; }
        }

        /// <summary>
        /// returns true when transaction is active
        /// </summary>
		public bool InTransaction
        {
            get { return _inTransaction || _inGlobalTransaction; }
        }

        public virtual void SetNewConnection(SynnDataAccessBase d)
        {
            if (_childList != null)
                ChildList.DisposeData(); // calling dispose on all children objects if exists
            if (ownConnection)
                _connection.Dispose(); // clears currently owned connection
            ownConnection = false;
            _inTransaction = d.InTransaction;
            _inGlobalTransaction = d.InGlobalTransaction;
            SetConnection(d._connection);
            SetTransaction(d._trans);
        }

        protected void setConnectionString(string connstr)
        {
            _connection.ConnectionString = connstr;
        }

        ~SynnDataAccessBase()
        {
            Dispose(false);
        }

        public void Open()
        {
            _connection.Open();
        }

        public void Close()
        {
            _connection.Close();
            _inTransaction = false;
            _inGlobalTransaction = false;
        }

        /// <summary>
        /// begins global transaction
        /// all calls for start/commit/rollback of "regular" transaction will be ignored
        /// only global transaction command will be processed
        /// </summary>
        /// <param name="l"></param>
        public void BeginGlobalTransaction(IsolationLevel l)
        {
            SetTransaction(_connection.BeginTransaction(l));
            _inGlobalTransaction = true;
        }

        /// <summary>
        /// check overload
        /// </summary>
        public void BeginGlobalTransaction()
        {
            SetTransaction(_connection.BeginTransaction());
            _inGlobalTransaction = true;
        }

        public void BeginTransaction()
        {
            if (InGlobalTransaction)
                return;
            SetTransaction(_connection.BeginTransaction());
            _inTransaction = true;
        }

        public void BeginTransaction(IsolationLevel l)
        {
            if (InGlobalTransaction)
            {
                if (_trans.IsolationLevel != l)
                    throw new ApplicationException("Global transaction isolation level does not match internal transaction level");
                return;
            }
            SetTransaction(_connection.BeginTransaction(l));
            _inTransaction = true;
        }

        public void Commit()
        {
            if (InGlobalTransaction)
                return;
            try
            {
                _trans.Commit();
                _inTransaction = false;
            }
            catch (Exception ex)
            {
                CheckException(ex);
            }
        }

        /// <summary>
        /// commits a global transaction
        /// </summary>
        public void CommitGlobal()
        {
            if (!InGlobalTransaction)
                throw new ApplicationException("Global transaction is not active");
            try
            {
                _trans.Commit();
                _inGlobalTransaction = false;
            }
            catch (Exception ex)
            {
                CheckException(ex);
            }
        }

        /// <summary>
        /// rolls back a global transaction
        /// </summary>
        public void RollBackGlobal()
        {
            if (!InGlobalTransaction)
                throw new ApplicationException("Global transaction is not active");
            _trans.Rollback();
            _inGlobalTransaction = false;
        }

        public void RollBack()
        {
            if (InGlobalTransaction)
                return;
            _trans.Rollback();
            _inTransaction = false;
        }

        private void CheckException(Exception ex)
        {
            if (ex.Message.ToString().ToUpper().IndexOf("KEY ") > 0)
                throw new DuplicateKeyException();
            else
                if (ex.Message.ToString().Contains("String or binary data would be truncated."))
                throw new DataTruncateException();
            else
                throw ex;
        }

        protected IDataReader DoSelect(string pSql)
        {
            SetSql(pSql);
            return DoSelect();
        }

        /// <summary>
        /// Performs a select statments with predefined sql set by SetSql methods
        /// returns IDataReader reference
        /// </summary>
        /// <returns></returns>
        protected virtual IDataReader DoSelect()
        {
            if (_command.Connection.State != ConnectionState.Open)
                _command.Connection.Open();
            _command.CommandText = RepalceParameterPlaceHolders(_command.CommandText);
            var reader = _command.ExecuteReader(CommandBehavior.SingleResult); // seems with the singleresult , it actually reads all rows when closing the datareader to look for more resultset 
            //_command.Connection.Close();
            return reader;
        }

        /// <summary>
        /// Executes the the supplied sql
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        protected int ExecuteSql(string pSql)
        {
            SetSql(pSql);
            return ExecuteSql();
        }

        /// <summary>
        /// returns record count for the specifed parameters
        /// Use has records when the count it self doesnt matter!!
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="keyFieldname"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        protected int GetRecordCount(string tablename, string keyFieldname, object keyValue)
        {
            SetParameters(keyValue);
            _command.CommandText = String.Format("select count(*) from {0} where {1}=?", tablename, keyFieldname);
            return (int)_command.ExecuteScalar();
        }

        public event DataAccessEvent OnAfterExecuteSql;
        public event DataAccessEvent OnBeforeExecuteSql;

        /// <summary>
        /// Executes the predefined sql set using SetSql methods
        /// </summary>
        /// <returns></returns>
        protected virtual int ExecuteSql()
        {
            try
            {
                if (_command.Connection.State != ConnectionState.Open)
                    _command.Connection.Open();
                if (OnBeforeExecuteSql != null)
                    OnBeforeExecuteSql(this);
                try
                {
                    _command.CommandText = RepalceParameterPlaceHolders(_command.CommandText);
                    var res = _command.ExecuteNonQuery();
                    return res;
                }
                finally
                {
                    if (OnAfterExecuteSql != null)
                        OnAfterExecuteSql(this);
                    _command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                CheckException(ex);
                throw;
            }
        }

        protected void SetUpdateSql(string tableName, SqlItemList setParameters, SqlItemList whereParameters)
        {
            if (setParameters.Count == 0)
                throw new ApplicationException("set paramters is empty");
            if (whereParameters.Count == 0)
                throw new ApplicationException("where paramters is empty");
            SetSqlFormat(
@"update {0}
  set {1}  
", tableName, GetCommaSetFieldParamList(setParameters.Select(x => x.FieldName).ToList()));
            SetParameters(setParameters.Select(x => x.FieldValue).ToArray());
            foreach (var i in whereParameters)
                AddSqlWhereField(i.FieldName, i.FieldValue);
        }

        protected string GetCommaSetFieldParamList(ICollection lst)
        {
            StringBuilder b = new StringBuilder();
            int counter = 0;
            foreach (string s in lst)
            {
                if (counter != 0 && counter < lst.Count)
                    b.Append(",");
                b.Append(s + "=?");
                counter++;
            }
            return b.ToString();
        }
        /// <summary>
        /// Check if there are records in the specified table corresponding with
        /// the supplied key, suppose to return only 1 record from the database
        /// should be usefull for Data Integrity Checks		
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool HasRecords(string tableName, string keyFieldName, object value)
        {
            SetSqlFormat("select {0} from {1} where {0}=?", keyFieldName, tableName);
            SetParameters(value);
            using (IDataReader data = _command.ExecuteReader(CommandBehavior.SingleRow))
            {
                return (data.Read());
            }
        }


        /// <summary>
        /// set sql text with format capiblities
        /// </summary>
        /// <param name="pSql"></param>
        /// <param name="parameters"></param>
        protected void SetSqlFormat(string pSql, params string[] parameters)
        {
            SetSql(string.Format(pSql, parameters));
        }

        /// <summary>
        /// adds text to the sql with format capbilites
        /// </summary>
        /// <param name="newTxt"></param>
        /// <param name="parameters"></param>
        protected void AddSqlTextFormat(string newTxt, params string[] parameters)
        {
            AddSqlText(string.Format(newTxt, parameters));
        }

        #region Or group

        int _orGroupCounter = -1;
        private const int ORGroupBaseCount = 1;

        enum OrGroupStartStyle { NoAnd, And }

        void StartORGroup(OrGroupStartStyle style)
        {
            _orGroupCounter = ORGroupBaseCount;
            string text = " (";
            if (style == OrGroupStartStyle.And)
                AddSqlWhereSqlText(text);
            else
                AddSqlText(text);
        }

        protected void StartORGroup()
        {
            StartORGroup(OrGroupStartStyle.And);
        }

        protected void StartORGroupNoAnd()
        {
            StartORGroup(OrGroupStartStyle.NoAnd);
        }

        private void StartOrField()
        {
            if (_orGroupCounter == -1)
                throw new ApplicationException("AddORField called without calling StartORGroup first");
            if (_orGroupCounter != ORGroupBaseCount)
                AddSqlText(" or ");
        }

        protected void AddORField(string fieldName, object val, string pOperator)
        {
            AddOrText(string.Format("{0} {1} ?", fieldName, pOperator), val);
        }

        protected void AddOrText(string txt)
        {
            StartOrField();
            AddSqlText(txt);
            _orGroupCounter++;
        }

        protected void AddOrText(string txt, object val)
        {
            StartOrField();
            AddSqlText(txt);
            AddParameters(val);
            _orGroupCounter++;
        }

        protected void AddORFieldIsNull(string fieldName)
        {
            if (_orGroupCounter == -1)
                throw new ApplicationException("AddORField called without calling StartORGroup first");
            if (_orGroupCounter != ORGroupBaseCount)
                AddSqlText(" or ");
            AddSqlText(string.Format("{0} is Null", fieldName));
            _orGroupCounter++;
        }

        protected void AddORFieldIsNotNull(string fieldName)
        {
            if (_orGroupCounter == -1)
                throw new ApplicationException("AddORField called without calling StartORGroup first");
            if (_orGroupCounter != ORGroupBaseCount)
                AddSqlText(" or ");
            AddSqlText(string.Format("not {0} is Null", fieldName));
            _orGroupCounter++;
        }

        protected void AddORLikeField(string fieldName, string val, LikeSelectionStyle style)
        {
            val = GetLikeParameterString(style, val);
            AddORField(fieldName, val, "like");
        }

        protected void AddOREqualField(string fieldName, object val)
        {
            AddORField(fieldName, val, "=");
        }

        protected void EndORGroup()
        {
            if (_orGroupCounter > ORGroupBaseCount)
                AddSqlText(")");
            _orGroupCounter = -1;
        }

        #endregion

        protected void AddSqlText(string pSql)
        {
            _command.CommandText += string.Format(" {0} ", pSql);
        }

        protected void ClearParameters()
        {
            ResetSqlWhereFieldCounter(); // make sense , if we clear the parameter we dont have sql where field anyhow						
            ResetSqlSetfieldCounter();
            ResetOrderbyFieldCounter();
            _command.Parameters.Clear();
        }

        protected void AddParamter(object value)
        {
            _command.Parameters.Add(_dataProvider.getParameter(value));
        }

        protected void AddParameters(params object[] parameters)
        {
            foreach (object x in parameters)
            {
                AddParamter(x);
            }

        }

        /// <summary>
        /// clears the old sql statement and set the parameter as the new one
        /// </summary>
        /// <param name="pSql"></param>
        protected void SetSql(string pSql)
        {
            _command.CommandText = pSql;
        }

        /// <summary>
        /// add a where field to the sql with equal sign
        /// </summary>
        /// <param name="pFieldName"></param>
        /// <param name="pValue"></param>
        protected void AddSqlWhereField(string pFieldName, object pValue)
        {
            AddSqlWhereField(pFieldName, pValue, "=");
        }

        /// <summary>
        /// add a where field to the sql with notequal sign
        /// </summary>
        /// <param name="pFieldName"></param>
        /// <param name="pValue"></param>
        protected void AddSqlWhereNotEqualField(string pFieldName, object pValue)
        {
            AddSqlWhereField(pFieldName, pValue, "<>");
        }

        /// <summary>
        /// add a where field to the sql with compare sign as parameter
        /// </summary>
        /// <param name="pFieldName"></param>
        /// <param name="pValue"></param>
        protected void AddSqlWhereField(string pFieldName, object pValue, string compareSign)
        {
            AddSqlWhereSqlTextFormat("{0}{1}{2}", pFieldName, compareSign, "?");
            AddParamter(pValue);
        }

        /// <summary>
        /// adds free form sql text to the current statment
        /// </summary>
        /// <param name="text"></param>
        protected void AddSqlWhereSqlText(string text)
        {
            if (_whereFieldCounter++ == 0)
                AddSqlTextFormat("Where {0}", text);
            else
                AddSqlTextFormat("and {0}", text);
        }

        protected void AddSqlWhereInGroup(string fieldName, IEnumerable list)
        {
            StartORGroup();
            foreach (object o in list)
                AddOREqualField(fieldName, o);
            EndORGroup();
        }

        /// <summary>
        /// adds a where field to sql in which field compared to null
        /// </summary>
        /// <param name="fieldName"></param>
        protected void AddSqlWhereFieldIsNull(string fieldName)
        {
            AddSqlWhereSqlTextFormat("{0} is NULL", fieldName);
        }

        /// <summary>
        /// checks if field is not null
        /// </summary>
        /// <param name="fieldName"></param>
        protected void AddSqlWhereFieldIsNotNull(string fieldName)
        {
            AddSqlWhereSqlTextFormat("not ({0} is NULL)", fieldName);
        }

        /// <summary>
        /// adds free form sql text to the current statment
        /// with formating support
        /// </summary>
        /// <param name="text"></param>
        protected void AddSqlWhereSqlTextFormat(string text, params object[] parameters)
        {
            AddSqlWhereSqlText(string.Format(text, parameters));
        }

        public enum LikeSelectionStyle
        {
            /// <summary>
            /// use the value as is , normally for complex like strings with _ chars and such
            /// </summary>
            AsIs,
            /// <summary>
            /// appends a % sign to the end of the text
            /// </summary>
            CheckEnd,
            /// <summary>
            /// adds a % sign at the start of the text
            /// </summary>
            CheckStart,
            /// <summary>
            /// adds a % sign both at the start and the end of the text
            /// </summary>
            CheckBoth
        }

        /// <summary>
        /// adds a where like field with the style of the like phrase
        /// </summary>
        /// <param name="pFieldName"></param>
        /// <param name="pValue"></param>
        /// <param name="style"></param>
        protected void AddSqlWhereLikeField(string pFieldName, string pValue, LikeSelectionStyle style)
        {
            AddSqlWhereSqlTextFormat("{0} {1}", pFieldName, "like ?");
            string val = (string)pValue;
            val = GetLikeParameterString(style, val);
            AddParamter(val);
        }

        private static string GetLikeParameterString(LikeSelectionStyle style, string val)
        {
            switch (style)
            {
                case LikeSelectionStyle.CheckBoth:
                    val = string.Format("%{0}%", val);
                    break;
                case LikeSelectionStyle.CheckEnd:
                    val = string.Format("{0}%", val);
                    break;
                case LikeSelectionStyle.CheckStart:
                    val = string.Format("%{0}", val);
                    break;
            }
            return val;
        }

        /// <summary>
        /// calls AddSqlWhereLikeField with LikeSelectionStyle.CheckBoth
        /// </summary>
        /// <param name="pFieldName"></param>
        /// <param name="pValue"></param>
        protected void AddSqlWhereLikeField(string pFieldName, string pValue)
        {
            AddSqlWhereLikeField(pFieldName, pValue, LikeSelectionStyle.CheckBoth);
        }

        protected void SetParameters(params object[] parameters)
        {
            ClearParameters();
            AddParameters(parameters);
        }

        public ConnectionState DataConnectionState
        {
            get
            {
                return _connection.State;
            }
        }

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _command.Dispose();
                    if (ownConnection)
                        _connection.Dispose();
                }
            }
            disposed = true;
            _inTransaction = false;
            _inGlobalTransaction = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void AddStreamParam(Stream data)
        {
            if (data.Length <= 0)
                AddParameters(DBNull.Value);
            else
            {
                byte[] buf = new byte[data.Length];
                data.Position = 0;
                data.Read(buf, 0, (int)data.Length);
                AddParameters(buf);
            }
        }

        protected void ResetSqlSetfieldCounter()
        {
            ResetSqlSetfieldCounter(0);
        }

        protected void ResetSqlSetfieldCounter(int val)
        {
            _setFieldCounter = val;
        }

        protected void ResetSqlWhereFieldCounter(int val)
        {
            _whereFieldCounter = val;
        }

        protected void ResetSqlWhereFieldCounter()
        {
            ResetSqlWhereFieldCounter(0);
        }

        protected int SqlSetFieldCounter
        {
            get { return _setFieldCounter; }
        }

        protected int SqlWhereFieldCounter
        {
            get { return _whereFieldCounter; }
        }

        protected void AddSqlSetfield(string fieldName, string pValue)
        {
            if (_setFieldCounter++ == 0)
                AddSqlTextFormat(" set {0}={1}", fieldName, pValue);
            else
                AddSqlTextFormat(",{0}={1}", fieldName, pValue);
        }

        protected void ResetOrderbyFieldCounter()
        {
            _orderbyFieldCounter = 0;
        }

        protected int OrderbyFieldCounter
        {
            get { return _orderbyFieldCounter; }
        }

        protected void AddOrderbyField(string fieldname, bool ascending)
        {
            AddOrderbyField(fieldname, ascending == true ? "asc" : "desc");
        }

        /// <summary>
        /// note , adding order by fields should be done at the "end" of the sql and in group
        /// </summary>
        /// <param name="fieldname"></param>
        protected void AddOrderbyField(string fieldname)
        {
            AddOrderbyField(fieldname, null);
        }

        public string ConnectionId
        {
            get
            {
                IDbCommand command = DataProvider.getCommand();
                command.CommandText = "SELECT @@SPID";
                command.Connection = _connection;
                if (InTransaction)
                    command.Transaction = _trans;
                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    return reader[0].ToString();
                return null;

            }
        }

        void AddOrderbyField(string fieldname, string direction)
        {
            if (_orderbyFieldCounter == 0)
                AddSqlTextFormat("order by {0}", fieldname);
            else
                AddSqlTextFormat(",{0}", fieldname);
            if (direction != null)
                AddSqlTextFormat("{0}", direction);
            _orderbyFieldCounter++;
        }

        /// <summary>
        /// calls DoSelect() and returns only the first record on the result set
        /// </summary>
        /// <param name="d"></param>
        protected void LoadSingleRecord(IDataLoadable d)
        {
            using (IDataReader data = DoSelect())
            {
                if (data.Read())
                    d.Load(data);
                else
                    throw new NoRecordException();
            }
        }

        /// <summary>
        /// create Datareader using doselect and reutrns true if there are results false if there are not
        /// this is usually more efficent than GetRecordCount  (when the count itself doesnt matter)
        /// </summary>
        /// <returns></returns>
        protected bool HasRecords()
        {
            using (IDataReader data = DoSelect())
            {
                return data.Read();
            }
        }

        int? _lastSetIdentityValue;

        protected void SetLastIdentityValue(int val)
        {
            _lastSetIdentityValue = val;
        }

        /// <summary>
        /// Currently works only for ms sql
        /// </summary>
        /// <returns></returns>
        public virtual int GetMsSqlLastIdentityValue()
        {
            if (_lastSetIdentityValue.HasValue)
            {
                try
                {
                    return _lastSetIdentityValue.Value;
                }
                finally
                {
                    _lastSetIdentityValue = null;
                }
            }
            SetSql("SELECT @@IDENTITY");
            ClearParameters();
            using (IDataReader data = DoSelect())
            {
                if (data.Read())
                {
                    if (data.IsDBNull(0))
                        throw new ApplicationException("Scope identity is null");
                    else
                        return Convert.ToInt32(data.GetDecimal(0));
                }
            }
            throw new ApplicationException("no identity found");
        }

        /// <summary>
        /// gets the specified fields next value (+1)
        /// requires repeatable read or Serializable transaction to be active
        /// caller is resposible to commit or rollback the transaction
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        protected int GetNumeratorFieldNextValue(string tableName, string fieldName, string keyField, int keyValue)
        {
            if (!InTransaction || (_trans.IsolationLevel != IsolationLevel.RepeatableRead && _trans.IsolationLevel != IsolationLevel.Serializable))
                throw new ApplicationException("GetNumeratorFieldNextValue must be invoked within repeatable read or Serializable transaction");
            int val = GetNumeratorCurrentValue(tableName, fieldName, keyField, keyValue);// locks the row 
            val++;
            SetSqlFormat("update {0} set {1} = ? where {2} = ?", tableName, fieldName, keyField);
            SetParameters(val, keyValue);
            if (ExecuteSql() != 1)
                throw new ApplicationException("Error while updaing in GetNumeratorFieldNextValue");
            return val;
        }

        /// <summary>
        /// gets the current value of the numerator in the database
        /// DO NOT USE THIS METHOD FOR INCREMENTING THE NUMERATOR !!!
        /// use GetNumeratorFieldNextValue for this purpose
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        protected int GetNumeratorCurrentValue(string tableName, string fieldName, string keyField, int keyValue)
        {
            SetParameters(keyValue);
            SetSqlFormat("select {0} from {1} where {2} = ?", fieldName, tableName, keyField);
            return (int)GetSingleRecordFirstValue();
        }

        /// <summary>
        /// Updates new seed value for the sepcified numerator
        /// requires old value to check consistensy
        /// throws exception if old value not equal to the value on database
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected void UpdateNumeratorSeed(string tableName, string fieldName, int oldValue, int newValue, string keyField, int keyValue)
        {
            UpdateNumeratorSeedWithNulls(tableName, fieldName, oldValue, newValue, keyField, keyValue);
        }

        /// <summary>
        /// Updates new seed value for the sepcified numerator
        /// requires old value to check consistensy
        /// throws exception if old value not equal to the value on database
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected void UpdateNumeratorSeedWithNulls(string tableName, string fieldName, int? oldValue, int? newValue, string keyField, int keyValue)
        {
            BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                if (oldValue.HasValue)
                {
                    int val = GetNumeratorCurrentValue(tableName, fieldName, keyField, keyValue); // locks the row                
                    if (val != oldValue)
                        throw new ApplicationException("Old value is incorrect for UpdateNumeratorSeed");
                }
                else
                    try
                    {
                        int val = GetNumeratorCurrentValue(tableName, fieldName, keyField, keyValue); // locks the row                
                        // if it reachs here , it means there is value and oldvalue should not be null
                        throw new ApplicationException("Old value is incorrect for UpdateNumeratorSeed");

                    }
                    catch (NoRecordException)
                    {
                        // o.k , no record exist which means its o.k for oldvalue to be null
                    }
                if (newValue.HasValue)
                {
                    if (oldValue.HasValue)
                    {
                        SetSqlFormat("update {0} set {1} = ? where {2} = ?", tableName, fieldName, keyField);
                        SetParameters(newValue, keyValue);
                        if (ExecuteSql() != 1)
                            throw new ApplicationException("Error while updaing in UpdateNumeratorSeed");
                    }
                    else
                    {
                        SetSqlFormat("insert into {0} ({1},{2}) values (?,?)", tableName, fieldName, keyField);
                        SetParameters(newValue, keyValue);
                        if (ExecuteSql() != 1)
                            throw new ApplicationException("Error while updaing in UpdateNumeratorSeed");
                    }
                }
                else
                {
                    SetSqlFormat("delete {0} where {1} = ?", tableName, keyField);
                    SetParameters(keyValue);
                    if (ExecuteSql() != 1)
                        throw new ApplicationException("Error while updaing in UpdateNumeratorSeed");
                }
                Commit();
            }
            catch
            {
                RollBack();
                throw;
            }
        }

        protected object GetSingleRecordFirstValue()
        {
            using (IDataReader data = DoSelect())
            {
                if (data.Read())
                    return data.GetValue(0);
                else
                    throw new NoRecordException();
            }
        }

        public IEnumerable FillList(Type listType, Type itemType)
        {
            IList lst = (IList)Activator.CreateInstance(listType, null);
            return FillList(itemType, lst);
        }

        private IEnumerable FillList(Type itemType, IList lst)
        {
            using (IDataReader data = DoSelect())
            {
                while (data.Read())
                {
                    var item = Activator.CreateInstance(itemType, data);
                    lst.Add(item);
                }
            }
            return lst;
        }

        public IEnumerable FillList(IList lst, Type itemType)
        {
            return FillList(itemType, lst);
        }

        /// <summary>
        /// thrown when database report on duplicate key
        /// </summary>
        public class DuplicateKeyException : ApplicationException
        {
            public DuplicateKeyException() : base("Duplicate key on database")
            {

            }
        }

        /// <summary>
        /// thrown when sql server throws an error that data in parameter wont fit in the database
        /// </summary>
        public class DataTruncateException : ApplicationException
        {
            public DataTruncateException() : base("Sql data truncation exception")
            {

            }
        }

        public class DataIntegrityException : ApplicationException
        {
            const string DataIntegrityMessage = "לא ניתן למחוק את הפריט, הוא נמצא בשימוש במערכת";

            public DataIntegrityException() : base(DataIntegrityMessage)
            {
            }

            protected DataIntegrityException(string message)
                : base(message)
            { }
        }

        public class NoRecordException : ApplicationException
        {
            const string message = "Record not found";

            public NoRecordException() : base(message)
            {
            }
        }

        public interface IDataLoadable
        {
            void Load(IDataReader data);
        }

        public class DataAcessBaseChildList : List<SynnDataAccessBase>
        {
            SynnDataAccessBase _owner;

            public DataAcessBaseChildList(SynnDataAccessBase owner)
            {
                _owner = owner;
            }

            public T GetInstance<T>()
            {
                foreach (SynnDataAccessBase d in this)
                    if (d is T)
                    {
                        ((SynnDataAccessBase)d).SetNewConnection(_owner);
                        return (T)(object)d;
                    }
                T inst = (T)Activator.CreateInstance(typeof(T), _owner.DataProvider);
                SynnDataAccessBase newDb = (SynnDataAccessBase)(object)inst;
                newDb.SetNewConnection(_owner);
                this.Add(newDb);
                return inst;
            }

            public void DisposeData()
            {
                foreach (SynnDataAccessBase d in this)
                    d.Dispose();
            }
        }

        public delegate void DataAccessEvent(SynnDataAccessBase d);

        public string GetSp_executesqlText()
        {
            var sb = new StringBuilder(string.Format("exec sp_executesql N'{0}'", RepalceParameterPlaceHolders(_command.CommandText)));
            var paramList = new List<string>();
            var valueList = new List<string>();
            int counter = 1;
            foreach (System.Data.Common.DbParameter p in _command.Parameters)
            {
                paramList.Add(string.Format("@p{0} {1}", counter, GetDbTypeText(p)));
                valueList.Add(GetDbValueText(p));
                counter++;
            }
            if (paramList.Count > 0)
            {
                sb.AppendFormat(",N'");
                for (int i = 0; i < paramList.Count; i++)
                    sb.AppendFormat("{0},", paramList[i]);
                sb.Remove(sb.Length - 1, 1);
                sb.AppendFormat("'");
                for (int i = 0; i < valueList.Count; i++)
                    sb.AppendFormat(",{0}", valueList[i]);
            }
            return sb.ToString();
        }

        private string RepalceParameterPlaceHolders(string sql)
        {
            char keyToReplace = '?';
            char newKey = '$';
            sql = sql.Replace(keyToReplace, newKey);
            keyToReplace = newKey;

            var sb = new StringBuilder(sql);
            int counter = 0;
            while (sb.ToString().IndexOf(keyToReplace) >= 0)
            {
                var idx = sb.ToString().IndexOf(keyToReplace);
                sb.Remove(idx, 1);
                sb.Insert(idx, string.Format("{0}", GetDbValueText((SqlParameter)_command.Parameters[counter])));
                counter++;
            }
            _command.Parameters.Clear();
            string res = sb.ToString();

            return res;
        }

        private string GetDbValueText(System.Data.Common.DbParameter p)
        {
            if (p.Value == null || p.Value == DBNull.Value)
                return "null";
            if (p is System.Data.Odbc.OdbcParameter && p.Value != null)
            {
                var t = p.Value.GetType();
                if (p.Value is Enum)
                    return ((int)p.Value).ToString();
                if (t == typeof(int))
                    return p.Value.ToString();
                if (t == typeof(short))
                    return p.Value.ToString();
                if (t == typeof(long))
                    return p.Value.ToString();
                if (t == typeof(string))
                    return "'" + p.Value.ToString().Replace("'", "''") + "'";
                if (t == typeof(DateTime))
                    return string.Format("'{0}'", ((DateTime)p.Value).ToString("yyyy-MM-dd HH:mm:ss"));
                if (t == typeof(bool))
                    return (bool)p.Value ? "1" : "0";
                if (t == typeof(decimal))
                    return p.Value.ToString();
                if (t == typeof(double))
                    return p.Value.ToString();
                return "unknown";
            }
            else
                switch (p.DbType)
                {
                    case DbType.Int32:
                        if (p.Value is Enum)
                            return ((int)p.Value).ToString();
                        return p.Value.ToString();
                    case DbType.Int16: return p.Value.ToString();
                    case DbType.Int64: return p.Value.ToString();
                    case DbType.String: return "'" + p.Value.ToString().Replace("'", "''") + "'";
                    case DbType.AnsiString: return "'" + p.Value.ToString().Replace("'", "''") + "'";
                    case DbType.DateTime: return string.Format("'{0}'", ((DateTime)p.Value).ToString("yyyy-MM-dd HH:mm:ss"));
                    case DbType.Boolean: return (bool)p.Value ? "1" : "0";
                    case DbType.Decimal: return p.Value.ToString();
                    case DbType.Double: return p.Value.ToString();
                    default: return "unknown";
                }
        }


        private string GetDbTypeText(System.Data.Common.DbParameter p)
        {
            if (p is System.Data.Odbc.OdbcParameter && p.Value != null)
            {
                var t = p.Value.GetType();
                if (t == typeof(int) || p.Value is Enum)
                    return "int";
                if (t == typeof(short))
                    return "small";
                if (t == typeof(long))
                    return "long";
                if (t == typeof(string))
                    return string.Format("nvarchar({0})", ((string)p.Value).Length);
                if (t == typeof(DateTime))
                    return "datetime";
                if (t == typeof(bool))
                    return "bit";
                if (t == typeof(decimal))
                    return "decimal";
                if (t == typeof(double))
                    return "double";
                return "unknown";
            }
            else
                switch (p.DbType)
                {
                    case DbType.Int32: return "int";
                    case DbType.Int16: return "small";
                    case DbType.Int64: return "long";
                    case DbType.String: return string.Format("nvarchar({0})", p.Size);
                    case DbType.AnsiString: return string.Format("nvarchar({0})", p.Size);
                    case DbType.DateTime: return "datetime";
                    case DbType.Boolean: return "bit";
                    case DbType.Decimal: return "decimal";
                    case DbType.Double: return "double";
                    default: return "unknown";
                }
        }

    }

}