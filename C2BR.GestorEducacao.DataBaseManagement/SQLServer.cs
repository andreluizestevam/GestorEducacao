//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Data;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.DatabaseManagement
{
    public class SQLServer
    {
        #region Constructors

        private SQLServer() { }

        /// <summary>
        /// Initializes SQLServer class with the CurrentSQLServer as the first local SQL Server Instance and the CurrentDatabaseName, if have.
        /// </summary>
        /// <param name="database">The database name</param>
        public SQLServer(string database)
        {
            this.CurrentDatabaseName = database;
            this.CurrentSQLServer = GetSQLServer();
        }

        /// <summary>
        /// Initializes SQLServer class
        /// </summary>
        /// <param name="database">The database name</param>
        public SQLServer(string database, Server sqlServer)
        {
            this.CurrentDatabaseName = database;
            this.CurrentSQLServer = sqlServer;
        }

        /// <summary>
        /// Initializes SQLServer class with the CurrentSQLServer as the result of serverConnection
        /// </summary>
        /// <param name="database">The database name</param>
        /// <param name="serverConnection">A Microsoft.SqlServer.Management.Common.ServerConnection with SQL Server conection parameters.</param>
        public SQLServer(string database, ServerConnection serverConnection)
            : this(database, GetSQLServer(serverConnection)) { }

        /// <summary>
        /// Initializes SQLServer class with the CurrentSQLServer as the result of sqlConnection
        /// </summary>
        /// <param name="database">The database name</param>
        /// <param name="sqlConnection">A System.Data.SqlClient.SqlConnection with SQL Server conection parameters.</param>
        public SQLServer(string database, SqlConnection sqlConnection)
            : this(database, GetSQLServer(new ServerConnection(sqlConnection))) { }

        /// <summary>
        /// Initializes SQLServer class with the CurrentSQLServer as the result of connectionString
        /// </summary>
        /// <param name="database">The database name</param>
        /// <param name="sqlConnection">A System.String with SQL Server conection parameters.</param>
        public SQLServer(string database, string connectionString)
            : this(database, new SqlConnection(connectionString)) { }

        #endregion

        #region Propriedades

        public Server CurrentSQLServer { get; set; }

        public string CurrentDatabaseName { get; set; }

        #endregion

        #region Métodos

        #region Métodos Estáticos        

        /// <summary>
        /// Get the Backup History of specifed database.
        /// </summary>
        /// <param name="database">A System.String value to represent the name of the database to get information about backup set history.</param>
        /// <param name="server">A Microsoft.SqlServer.Management.Smo.Server where the database is located.</param>
        /// <returns>A System.Data.DataTable object containing all backup history.</returns>
        public static DataTable GetBackupSetHistory(string database, Server server)
        {
            return server.Databases[database].EnumBackupSets();
        }

        /// <summary>
        /// Get the Backup History of specifed database.
        /// </summary>
        /// <param name="database">A System.String value to represent the name of the database to get information about backup set history.</param>
        /// <param name="backupSetNameFilter">A System.String value to filter the Backup Set Name.</param>
        /// <param name="server">A Microsoft.SqlServer.Management.Smo.Server where the database is located.</param>
        /// <returns>A System.Data.DataTable object containing backup history where Backup Set Name CONTAINS the backupSetNameFilter specified in parameters.</returns>
        public static DataTable GetBackupSetHistory(string database, string backupSetNameFilter, Server server)
        {
            DataTable backupHistory = GetBackupSetHistory(database, server);
            DataTable filteredBackupHistory = backupHistory.Clone();
            DataRow[] filteredRows = backupHistory.Select("Name LIKE '%" + backupSetNameFilter + "%'");

            foreach (DataRow row in filteredRows)
                filteredBackupHistory.ImportRow(row);

            return filteredBackupHistory;
        }        

        /// <summary>
        /// Get information about the backup device especified in the parameters.
        /// </summary>
        /// <param name="backupDeviceItem">A Microsoft.SqlServer.Management.Smo.backupDeviceItem to specify the device to get backup information.</param>
        /// <param name="server">A Microsoft.SqlServer.Management.Smo.Server where the backup device is located.</param>
        public static DataTable ReadBackupHeader(BackupDeviceItem backupDeviceItem, Server server)
        {
//--------> Cria nova instância da classe Restore
            Restore restore = new Restore();

//--------> Adiciona dispositivos para obter informações
            restore.Devices.Add(backupDeviceItem);

//--------> Faz a leitura do Backup Header e retorna a informação como DataTable
            return restore.ReadBackupHeader(server);
        }

        /// <summary>
        /// Get the first local SQL Server Instance, if have.
        /// </summary>
        /// <returns>Returns a Microsoft.SqlServer.Management.Smo.Server instance, representing the first local SQL Server Instance.</returns>
        public static Server GetSQLServer()
        {
            string strServerInstanceName = "";

//--------> Faz a verificação se tem alguma instância local do SQL Server
            if (SqlServerRegistrations.RegisteredServers.Count > 0)
//------------> Obtem a primeira instância do servidor SQL Local
                strServerInstanceName = SqlServerRegistrations.RegisteredServers[0].ServerInstance;

//--------> Faz a verificação se um nome de instância foi encontrado
            if (String.IsNullOrEmpty(strServerInstanceName))
                // If no Instance was found, return a new empty server
                return new Server();

//--------> Se existir uma instância com esse nome, retorna o servidor baseado no mesmo
            return GetSQLServer(new ServerConnection(strServerInstanceName));
        }

        /// <summary>
        /// Get a SQL Server with the especified connection.
        /// </summary>
        /// <param name="connection">A Microsoft.SqlServer.Management.Commom.ServerConnection value that specifies the connection of SQL Server.</param>
        /// <returns>Returns a Microsoft.SqlServer.Management.Smo.Server instance.</returns>
        public static Server GetSQLServer(ServerConnection connection)
        {
//--------> Faz a criação de um novo servidor c a informação de ServerConnection informada
            return new Server(connection);
        }

        /// <summary>
        /// Get a SQL Server with the especified connection.
        /// </summary>
        /// <param name="sqlConnection">A System.Data.SqlClient.SqlConnection value that specifies the connection.</param>
        /// <returns>Returns a Microsoft.SqlServer.Management.Smo.Server instance.</returns>
        public static Server GetSQLServer(SqlConnection sqlConnection)
        {
            
            ServerConnection cnn = new ServerConnection(sqlConnection);
            return new Server(cnn);
        }

        #endregion Métodos Estáticos

        #endregion Métodos

        #region Restore

        /// <summary>
        /// Restore Database
        /// </summary>
        /// <param name="database"></param>
        /// <param name="replaceDatabase"></param>
        /// <param name="fileNumber"></param>
        /// <param name="restoreActionType"></param>
        /// <param name="backupDeviceItem"></param>
        /// <param name="server"></param>
        public static void Restore(string database, bool replaceDatabase, int fileNumber, RestoreActionType restoreActionType, BackupDeviceItem backupDeviceItem, Server server)
        {
//--------> Faz a verificação para saber se uma base de dados foi informada
            if (String.IsNullOrEmpty(database))
                throw new ArgumentException("database");
            else if (fileNumber <= 0)
                throw new ArgumentException("fileNumber must be greater than 1");

            try
            {
                Restore restore = new Restore();
                restore.ReplaceDatabase = replaceDatabase;
                restore.Database = database;
                restore.Action = restoreActionType;
                restore.Devices.Add(backupDeviceItem);
                restore.FileNumber = fileNumber;
                restore.SqlRestore(server);
            }
            catch (FailedOperationException ex)
            {
                if (ex.InnerException != null)
                    if (ex.InnerException.InnerException != null)
//--------------------> Código Erro -2146232060 -> Banco de dados está em uso
                        if (((SqlException)ex.InnerException.InnerException).ErrorCode.Equals(-2146232060))
                            throw ex.InnerException.InnerException;
            }
        }

        /// <summary>
        /// Restore Database
        /// </summary>
        /// <param name="fileNumber"></param>
        public void Restore(int fileNumber)
        {
            Restore(this.CurrentDatabaseName, true, fileNumber, RestoreActionType.Database, new BackupDeviceItem(String.Format("{0}.fromCode.bak", this.CurrentDatabaseName), DeviceType.File), this.CurrentSQLServer);
        }
        #endregion Restore

        #region Backup

        /// <summary>
        /// Execute a backup with the especified parameters.
        /// </summary>
        /// <param name="database">A System.String value that specifies the name of the Database to be backuped.</param>
        /// <param name="backupSetName">A System.String value that specifies the name of the backup set.</param>
        /// <param name="backupSetDescription">A System.String value that specifies the description of the backup set.</param>
        /// <param name="backupActionType"> A Microsoft.SqlServer.Management.Smo.BackupActionType to specify the action type to be used in backup.</param>
        /// <param name="backupDeviceList">A Microsoft.SqlServer.Management.Smo.BackupDeviceList to specify the devices to be used in backup.</param>
        /// <param name="server">A Microsoft.SqlServer.Management.Smo.Server where the backup will be executed.</param>
        public static void Backup(string database, string backupSetName, string backupSetDescription, BackupActionType backupActionType, BackupDeviceList backupDeviceList, Server server)
        {
//--------> Faz a verificação para saber se uma base de dados foi informada
            if (String.IsNullOrEmpty(database))
                throw new ArgumentException("database");
//--------> Faz a verificação para saber se um nome de backup da base de dados foi informado
            else if (String.IsNullOrEmpty(backupSetName))
                throw new ArgumentException("backupSetName");
//--------> Faz a verificação para saber se uma descrição do backup da base de dados foi informada
            else if (String.IsNullOrEmpty(backupSetDescription))
                backupSetDescription = "No Description especified.";

//--------> Faz a criação de uma nova classe Backup
            Backup bk = new Backup();

//--------> Define as propriedades do backup backupt properties
            bk.Database = database;
            bk.BackupSetName = backupSetName;
            bk.BackupSetDescription = backupSetDescription;
            bk.Action = backupActionType;

//--------> Adiciona dispositivos de backup
            foreach (BackupDeviceItem bkpDeviceItem in backupDeviceList)
                bk.Devices.AddDevice(bkpDeviceItem.Name, bkpDeviceItem.DeviceType);

//--------> Faz o backup
            bk.SqlBackup(server);

//--------> Limpa os dispositivos de backup
            bk.Devices.Clear();
        }

        /// <summary>
        /// Execute a backup with the especified parameters.
        /// </summary>
        /// <param name="database">A System.String value that specifies the name of the Database to be backuped.</param>
        /// <param name="backupSetName">A System.String value that specifies the name of the backup set.</param>
        /// <param name="backupSetDescription">A System.String value that specifies the description of the backup set.</param>
        /// <param name="backupActionType"> A Microsoft.SqlServer.Management.Smo.BackupActionType to specify the action type to be used in backup.</param>
        /// <param name="backupDeviceItem">A Microsoft.SqlServer.Management.Smo.BackupDeviceItem to specify the device to be used in backup.</param>
        /// <param name="server">A Microsoft.SqlServer.Management.Smo.Server where the backup will be executed.</param>
        public static void Backup(string database, string backupSetName, string backupSetDescription, BackupActionType backupActionType, BackupDeviceItem backupDeviceItem, Server server)
        {
//--------> Adiciona parâmetro de dispositivo para a lista
            BackupDeviceList bkpDeviceList = new BackupDeviceList();
            bkpDeviceList.Add(backupDeviceItem);

//--------> Faz o backup
            Backup(database, backupSetName, backupSetDescription, backupActionType, bkpDeviceList, server);
        }

        /// <summary>
        /// Execute a backup with the especified parameters.
        /// </summary>
        /// <param name="backupSetName">A System.String value that specifies the name of the backup set.</param>
        /// <param name="backupSetDescription">A System.String value that specifies the description of the backup set.</param>
        /// <param name="backupActionType"> A Microsoft.SqlServer.Management.Smo.BackupActionType to specify the action type to be used in backup.</param>
        /// <param name="backupDeviceList">A Microsoft.SqlServer.Management.Smo.BackupDeviceList to specify the devices to be used in backup.</param>
        public void Backup(string backupSetName, string backupSetDescription, BackupActionType backupActionType, BackupDeviceList backupDeviceList)
        {
            Backup(this.CurrentDatabaseName, backupSetName, backupSetDescription, backupActionType, backupDeviceList, this.CurrentSQLServer);
        }

        /// <summary>
        /// Execute a backup with the especified parameters.
        /// </summary>
        /// <param name="backupSetName">A System.String value that specifies the name of the backup set.</param>
        /// <param name="backupSetDescription">A System.String value that specifies the description of the backup set.</param>
        /// <param name="backupActionType"> A Microsoft.SqlServer.Management.Smo.BackupActionType to specify the action type to be used in backup.</param>
        /// <param name="backupDeviceItem">A Microsoft.SqlServer.Management.Smo.BackupDeviceItem to specify the device to be used in backup.</param>
        public void Backup(string backupSetName, string backupSetDescription, BackupActionType backupActionType, BackupDeviceItem backupDeviceItem)
        {
//--------> Adiciona parâmetro de dispositivo para a lista
            BackupDeviceList devices = new BackupDeviceList();
            devices.Add(backupDeviceItem);

            this.Backup(backupSetName, backupSetDescription, backupActionType, devices);
        }

        /// <summary>
        /// Execute a backup with the especified parameters.
        /// </summary>
        /// <param name="backupSetName">A System.String value that specifies the name of the backup set.</param>
        /// <param name="backupSetDescription">A System.String value that specifies the description of the backup set.</param>
        public void Backup(string backupSetName, string backupSetDescription)
        {
            Backup(backupSetName, 
                   backupSetDescription, 
                   BackupActionType.Database,
                   new BackupDeviceItem(String.Format("{0}.fromCode.bak", this.CurrentDatabaseName), DeviceType.File));
        }

        /// <summary>
        /// Execute a backup with the especified parameters.
        /// </summary>
        /// <param name="backupActionType"> A Microsoft.SqlServer.Management.Smo.BackupActionType to specify the action type to be used in backup.</param>
        /// <param name="backupDeviceItem">A Microsoft.SqlServer.Management.Smo.BackupDeviceItem to specify the device to be used in backup.</param>
        public void Backup(BackupActionType backupActionType, BackupDeviceItem backupDeviceItem)
        {
//--------> Adiciona parâmetro de dispositivo para a lista
            BackupDeviceList devices = new BackupDeviceList();
            devices.Add(backupDeviceItem);

            this.Backup(String.Format("{0}_BackupSet_{1}", this.CurrentDatabaseName, DateTime.Now.ToString("dd-MM-yyyy_HH-mm-s")),
                        String.Format("Backup of {0} - {1} {2}", this.CurrentDatabaseName, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()),
                        backupActionType,
                        devices);
        }

        /// <summary>
        /// Execute a backup with the especified parameters.
        /// </summary>
        /// <param name="backupDeviceItem">A Microsoft.SqlServer.Management.Smo.BackupDeviceItem to specify the device to be used in backup.</param>
        public void Backup(BackupDeviceItem backupDeviceItem)
        {
            this.Backup(BackupActionType.Database,
                        backupDeviceItem);
        }

        /// <summary>
        /// Execute a backup with the especified parameters.
        /// </summary>
        public void Backup()
        {
            this.Backup(new BackupDeviceItem(String.Format("{0}.fromCode.bak", this.CurrentDatabaseName),
                                             DeviceType.File));
        }
        #endregion Backup        
    }
}