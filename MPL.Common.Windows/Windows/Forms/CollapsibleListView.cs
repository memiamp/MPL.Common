using MPL.Common.Win32;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace MPL.Common.Windows.Forms
{
    /// <summary>
    /// A class that defines a collapsible List View control.
    /// </summary>
    public class CollapsibleListView : ListView
    {
        #region Constructors
        static CollapsibleListView()
        {
            _ListGroupIDMember = typeof(ListViewGroup).GetField("id", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        #endregion

        #region Declarations
        #region _Members_
        private static FieldInfo _ListGroupIDMember;

        #endregion
        #endregion

        #region Methods
        #region _Private_
        private int GetGroupID(ListViewGroup listViewGroup)
        {
            int ReturnValue = 0;
            object Result;

            Result = _ListGroupIDMember.GetValue(listViewGroup);
            if (Result != null && Result is int)
                ReturnValue = (int)Result;

            return ReturnValue;
        }

        private int[] GetGroupIDs()
        {
            int[] ReturnValue;

            ReturnValue = new int[Groups.Count];
            for (int i = 0; i < Groups.Count; i++)
                ReturnValue[i] = GetGroupID(Groups[i]);

            return ReturnValue;
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Setup the ListView so that all existing groups are collapsible.
        /// </summary>
        /// <remarks>This will only set the collapsible state on existing groups. If new groups are added, this method must be invoked again.</remarks>
        public void SetupCollapsibleGroups()
        {
            User32Integration.SetListViewGroupsCollapsible(Handle, GetGroupIDs());
        }

        #endregion
        #endregion
    }
}