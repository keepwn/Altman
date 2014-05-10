using System;

namespace Altman.ModelCore
{

    public class ShellExtraData
    {
        public ShellSqlType ShellSqlType;
        public string ShellSqlHost;
        public string ShellSqlUser;
        public string ShellSqlPwd;
        public string ShellSqlAdoConnection;

        public ShellExtraData(ShellSqlType shellsqltype, string shellsqlhost, string shellsqluser, string shellsqlpwd)
        {
            this.ShellSqlType = shellsqltype;
            this.ShellSqlHost = shellsqlhost;
            this.ShellSqlUser = shellsqluser;
            this.ShellSqlPwd = shellsqlpwd;
        }

        public ShellExtraData(string shellsqltype, string shellsqlhost, string shellsqluser, string shellsqlpwd)
        {
            this.ShellSqlType = StrToShellSqlType(shellsqltype);
            this.ShellSqlHost = shellsqlhost;
            this.ShellSqlUser = shellsqluser;
            this.ShellSqlPwd = shellsqlpwd;
        }

        public static ShellSqlType StrToShellSqlType(string type)
        {
            type = type.ToUpper();
            return (ShellSqlType)Enum.Parse(typeof(ShellSqlType), type);
        }
    }
}
