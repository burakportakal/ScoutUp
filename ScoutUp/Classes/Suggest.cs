using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoutUp.DAL;
using ScoutUp.ViewModels;

namespace ScoutUp.Classes
{
    public class Suggest
    {
        public List<FollowSuggestViewModel> FollowSuggest(int userId,ScoutUpDB _db,bool nearby=false)
        {
            var user = _db.Users.Find(userId);
            //benzerlik listesi
            List<Similarity> similarties = new List<Similarity>();
            //Geri dönecek takip listesi
            var usersToFollow = new List<FollowSuggestViewModel>();
            //user ın puan verdiği itemler ile diğer userların puan verdiği itemin kesişimi alınacak
            //birinin puan verip diğerinin vermediği ile ilgilenmiyoruz
            var usersRatedItems = _db.UserRatings.Where(e => e.UserID == userId).
                Select(e => e.CategoryItemID).ToArray();
            //Kullanıcının puanları öğelerin Listesi UserandRating classından oluşuyor bu liste.Kullanıcının puanladığı öğelerin diğer kullanıcılarla kesişimini alırken kullanılacak.
            //Puanlanan öğelere ve itemlere daha kolay ulaşım sağlar
            var usersRatedItemsAndRatings = _db.UserRatings.Where(e => e.UserID == userId).Select(e => new UserAndRating { UserId = e.UserID, ItemId = e.CategoryItemID, UserRating = e.UserRating }).ToList();
            //Kullanıcın zaten takip ettikleriyle işimiz yok.
            var followlist = _db.UserFollow.Where(e => e.UserID == userId).Select(e => e.UserBeingFollowedUserID)
                .ToArray();
            IQueryable<int> allUserIds;
            // followlananlar hariç tüm kullanıcıları çekiyoruz
            if (nearby)
            {
                 allUserIds = _db.Users.Where(e => e.UserID != userId).Where(e => !followlist.Contains(e.UserID)).Where(e=> e.UserCity==user.UserCity)
                    .Select(e => e.UserID);
            }
            else
            {
                 allUserIds = _db.Users.Where(e => e.UserID != userId).Where(e => !followlist.Contains(e.UserID))
                    .Select(e => e.UserID);
            }

            //tüm öğelerin ayrı ayrı rating ortalamasını hesaplar Up olarak geçiyor formülde
            var allRatingsDictionary = new Dictionary<int, float>();
            foreach (var itemId in usersRatedItems)
            {
                var allRatedItemsRating = _db.UserRatings.Where(e => e.CategoryItemID == itemId)
                    .Select(e => new UserAndRating { ItemId = e.CategoryItemID, UserRating = e.UserRating }).ToList();
                allRatingsDictionary.Add(itemId, ((float)allRatedItemsRating.Sum(e => e.UserRating) / allRatedItemsRating.Count));
            }
            //Diğer kullanıcıları çekiyoruz sadece kullanıcıyla aynı puanladığı itemler geliyor ????? 
            var other = _db.UserRatings.Where(e => allUserIds.Contains(e.UserID)).
                //Where(e => usersRatedItems.Contains(e.CategoryItemID)).
                Select(e => new UserAndRating { UserId = e.UserID, ItemId = e.CategoryItemID, UserRating = e.UserRating });
            //Diğer kullanıcı ratinglerine kolay ulaşmak için bir sözlük.
            var otherUsersAndRatings = new Dictionary<int, List<UserAndRating>>();
            foreach (var ids in allUserIds)
            {
                var list = new List<UserAndRating>();
                foreach (var userAndRating in other)
                {
                    if (ids == userAndRating.UserId)
                    {
                        list.Add(userAndRating);
                    }
                }
                otherUsersAndRatings.Add(ids, list);
            }
            //Kullanıcının rating ortalaması
            float userMean = (float)usersRatedItemsAndRatings.Sum(e => e.UserRating) / (float)usersRatedItemsAndRatings.Count;
            //Diğer kullanıcılar
            foreach (var otherUser in otherUsersAndRatings)
            {
                float otherUserMean = (float)otherUser.Value.Sum(e => e.UserRating) / (float)otherUser.Value.Count;
                float ortakPuanlanan = 0;
                float Proximity = 0;
                float Significance = 0;
                float Singularity = 0;
                float PSS = 0;
                float RoUser = 0;
                float RoOther = 0;
                float URP = 0;
                float JPSS = 0;
                float Jaccord = 0;
                //diğer kullanıcının Ratingleri
                foreach (var otherUserRating in otherUser.Value)
                {
                    foreach (var usersRated in usersRatedItemsAndRatings)
                    {
                        if (usersRated.ItemId == otherUserRating.ItemId)
                        {
                            Proximity = 1 - (1 / (1 + ((float)Math.Exp(-Math.Abs(usersRated.UserRating - otherUserRating.UserRating)))));
                            Significance = (1 / (1 + ((float)Math.Exp(-((Math.Abs(usersRated.UserRating - userMean)) * Math.Abs(otherUserRating.UserRating - otherUserMean))))));
                            Singularity = 1 - (1 / (1 + ((float)Math.Exp(-(Math.Abs((usersRated.UserRating + otherUserRating.UserRating) / 2) - allRatingsDictionary[usersRated.ItemId])))));
                            PSS += Proximity * Significance * Singularity;

                            ortakPuanlanan++;
                        }

                        RoUser += (float)(Math.Pow(usersRated.UserRating - userMean, 2) / usersRatedItemsAndRatings.Count);
                    }

                    RoOther += (float)(Math.Pow(otherUserRating.UserRating - otherUserMean, 2) / otherUser.Value.Count);
                }

                RoUser = (float)Math.Sqrt(RoUser);
                RoOther = (float)Math.Sqrt(RoOther);
                Jaccord = (ortakPuanlanan) / (otherUser.Value.Count * usersRatedItemsAndRatings.Count);

                JPSS = PSS * Jaccord;

                URP = 1 - (1 / (1 + (float)(Math.Exp(-(Math.Abs(userMean - otherUserMean) * Math.Abs((RoUser - RoOther)))))));

                float sim = JPSS * URP;
                similarties.Add(new Similarity { OtherUserID = otherUser.Key, UserID = userId, UsersSimilarity = sim, RatedByUsersCount = otherUser.Value.Count });
            }
            var similartiesOrdered = similarties.OrderByDescending(e => e.UsersSimilarity);

            foreach (var similarity in similartiesOrdered)
            {
                var tempUser = _db.Users.Find(similarity.OtherUserID);
                var temp = new FollowSuggestViewModel {UserId = similarity.OtherUserID, Name = tempUser.UserName + " " + tempUser.UserSurname,ImagePath = tempUser.UserProfilePhoto,
                    Similarity =Math.Round((1000* similarity.UsersSimilarity),2),UserCity = tempUser.UserCity};
                usersToFollow.Add(temp);
            }

            return usersToFollow;
        }
        public List<UsersToFollow> FollowSuggest1(int userId,ScoutUpDB _db)
        {
           
            //user ın puan verdiği itemler ile diğer userların puan verdiği itemin kesişimi alınacak
            //birinin puan verip diğerinin vermediği ile ilgilenmiyoruz
            var usersRatedItems = _db.UserRatings.Where(e => e.UserID == userId).
                Select(e => e.CategoryItemID).ToArray();
            var usersRatedItemsAndRatings = _db.UserRatings.Where(e => e.UserID == userId).Select(e => new UserAndRating { UserId = e.UserID, ItemId = e.CategoryItemID, UserRating = e.UserRating }).ToList();
            var followlist = _db.UserFollow.Where(e => e.UserID == userId).Select(e => e.UserBeingFollowedUserID)
                .ToArray();
            var allUserIds = _db.Users.Where(e => e.UserID != userId).Where(e => !followlist.Contains(e.UserID)).Select(e => e.UserID);

            var allRatedItems = _db.UserRatings.Select(e => e.CategoryItemID).ToArray();
            var ratedCount = new System.Collections.Generic.Dictionary<int, float>();
            for (int i = 0; i < usersRatedItems.Count(); i++)
            {
                ratedCount.Add(usersRatedItems[i],
                   (float)Math.Log((float)(allUserIds.Count() + 1)) / (float)allRatedItems.Count(e => e == usersRatedItems[i]));
            }
            var other = _db.UserRatings.Where(e => allUserIds.Contains(e.UserID)).
                Where(e => usersRatedItems.Contains(e.CategoryItemID)).
                Select(e => new UserAndRating { UserId = e.UserID, ItemId = e.CategoryItemID, UserRating = e.UserRating });

            var otherUsersAndRatings = new Dictionary<int, List<UserAndRating>>();
            foreach (var ids in allUserIds)
            {
                var list = new List<UserAndRating>();
                foreach (var userAndRating in other)
                {
                    if (ids == userAndRating.UserId)
                    {
                        list.Add(userAndRating);
                    }
                }
                otherUsersAndRatings.Add(ids, list);
            }
            float userMean = (float)usersRatedItemsAndRatings.Sum(e => e.UserRating) / (float)usersRatedItemsAndRatings.Count;
            List<Similarity> similarties = new List<Similarity>();
            var usersToFollow = new List<UsersToFollow>();
            foreach (var otherUser in otherUsersAndRatings)
            {
                float otherUserMean = (float)otherUser.Value.Sum(e => e.UserRating) / (float)otherUser.Value.Count;
                float ustToplamCarpim = 0;
                float altToplamCarpim1 = 0;
                float altToplamCarpim2 = 0;
                float ortakPuanlanan = 0;
                foreach (var otherUserRating in otherUser.Value)
                {
                    foreach (var usersRated in usersRatedItemsAndRatings)
                    {
                        if (usersRated.ItemId == otherUserRating.ItemId)
                        {
                            ustToplamCarpim += (float)ratedCount[usersRated.ItemId] * ((float)usersRated.UserRating - (float)userMean) * ((float)otherUserRating.UserRating - otherUserMean);
                            altToplamCarpim1 += ratedCount[usersRated.ItemId] * ((float)(float)Math.Pow(((float)usersRated.UserRating - userMean), 2));
                            altToplamCarpim2 += ratedCount[usersRated.ItemId] * (float)(float)Math.Pow((float)(otherUserRating.UserRating - otherUserMean), 2);
                            ortakPuanlanan++;
                            break;
                        }
                    }
                }

                //NaN engellemek için 0.001 ile toplandı
                float sim = ((float)(ustToplamCarpim) /
                             ((float)(Math.Sqrt(altToplamCarpim1)) * (float)Math.Sqrt(altToplamCarpim2)));
                sim = (float)(sim * ((float)(ortakPuanlanan) / usersRatedItems.Length));
                similarties.Add(new Similarity { OtherUserID = otherUser.Key, UserID = userId, UsersSimilarity = sim, RatedByUsersCount = otherUser.Value.Count });

            }
            var similartiesOrdered = similarties.OrderByDescending(e => e.UsersSimilarity);

            foreach (var similarity in similartiesOrdered)
            {
                var tempUser = _db.Users.Find(similarity.OtherUserID);
                var temp = new UsersToFollow(similarity.OtherUserID, tempUser.UserName + " " + tempUser.UserSurname, tempUser.UserProfilePhoto);
                usersToFollow.Add(temp);
            }

            return usersToFollow;
        }
    }
    public class Similarity
    {

        public int UserID { get; set; }
        public int OtherUserID { get; set; }
        public float UsersSimilarity { get; set; }
        public int RatedByUsersCount { get; set; }
    }
    public class UserAndRating
    {
        public int UserId;
        public int ItemId;
        public double UserRating { get; set; }
    }
}