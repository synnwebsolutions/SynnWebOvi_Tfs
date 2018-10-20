using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public enum ClientPagePermissions
    {
        [Description("מילון משתמש")]
        Dictionary,
        [Description("יומן")]
        Diary,
        [Description("משמרות")]
        Shifts,
        [Description("חתונה")]
        Wedding,
        [Description("קניות")]
        Shopping
    }
    [Serializable]
    public class UserSharedGroupPermissions
    {
        // client shared data
        public int PermissionId { get; set; }
        public int UserId { get; set; }
        public bool MainPermission { get; set; }
        public UserSharedGroupPermissions()
        {

        }
    }

    
}