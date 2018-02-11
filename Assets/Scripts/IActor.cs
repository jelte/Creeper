
namespace ProjectFTP
{
    interface IActor
    {
        #region methods
        void TakeDamage(int amount);
        #endregion

        #region properties
        Direction Facing
        {
            get;
        }

        bool IsAlive
        {
            get;
        }
        #endregion
    }
}
