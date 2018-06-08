namespace ScoutUp.Classes
{
    public class FollowingUsers
    {
        public string id;
        public string Name;
        public bool UserFollowing;
        public string ImagePath;

        public FollowingUsers(string id, string name,string imagePath)
        {
            this.id = id;
            Name = name;
            ImagePath = imagePath;
        }
        public FollowingUsers(string id, string name,bool userFollowing, string imagePath)
        {
            this.id = id;
            Name = name;
            ImagePath = imagePath;
            UserFollowing = userFollowing;
        }
    }
}