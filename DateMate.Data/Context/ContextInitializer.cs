using DateMate.Data.Entities;
using DateMate.Entities;
using System;
using System.Data.Entity;

namespace DateMate.Data.Context
{
	//	Dev test data.
	public class ContextInitializer : DropCreateDatabaseAlways<DateMateContext>
	{
		protected override void Seed(DateMateContext context)
		{
			SeedUsers(context);
			SeedMessages(context);
			SeedFriendships(context);
			SeedPictures(context);
			base.Seed(context);
		}

		// All passwords == hejsan.
		private static void SeedUsers(DateMateContext context)
		{
			context.Users.Add(new User
			{
				FirstName = "Olof",
				Surname = "von Snorbart",
				BirthDate = DateTime.Parse("1993-04-12"),
				LoginName = "watever",
				EmailAddress = "foo@bar.com",
				Password = "2824d4bdb81403823339738a43b08e3f7a627d944b8d66eac296ca46596c8ac8", // hejsan
				Salt = "fDFz+ZmC514=",
				Searchable = true
			});

			context.Users.Add(new User
			{
				FirstName = "Hugh",
				Surname = "Mungus",
				BirthDate = DateTime.Parse("1993-04-12"),
				LoginName = "hugge",
				EmailAddress = "foo@baz.com",
				Password = "7c4265d398ff39f9165492468b344ffc53052bde76148080ef32aeb10b204c3c", // hejsan
				Salt = "ukMPQU8iMWA=",
				Searchable = true
			});

			context.Users.Add(new User
			{
				FirstName = "Adolf",
				Surname = "Andersson",
				BirthDate = DateTime.Parse("1993-04-12"),
				LoginName = "aaa",
				EmailAddress = "foo@a.com",
				Password = "0df2aee9942676b4b912a127989686566a9e37c11e9fb908a21c9ab6c99879d3",
				Salt = "j39dQOu4vbA=",
				Searchable = true
			});

			context.Users.Add(new User
			{
				FirstName = "Bertil",
				Surname = "Björk",
				BirthDate = DateTime.Parse("1993-04-12"),
				LoginName = "bbb",
				EmailAddress = "foo@b.com",
				Password = "860e994e4235c837ecdef2c57ef80869460810015b8e96980e1077678085f6ad",
				Salt = "i9lK0y6uzvo=",
				Searchable = true
			});

			context.Users.Add(new User
			{
				FirstName = "Cecilia",
				Surname = "Carlson",
				BirthDate = DateTime.Parse("1993-04-12"),
				LoginName = "ccc",
				EmailAddress = "foo@c.com",
				Password = "aa4870642145f77e96e2b391b9d3714160d2f080a4d9f5c7f7f597cb16ee8ea9",
				Salt = "vBtz/RsVJKM=",
				Searchable = true
			});

			context.Users.Add(new User
			{
				FirstName = "Daniel",
				Surname = "Davidsson",
				BirthDate = DateTime.Parse("1993-04-12"),
				LoginName = "ddd",
				EmailAddress = "foo@d.com",
				Password = "aa600ebcf0fd133c715055dedb73f6383d5e1ef24e0c4e1202885e57acabc32d",
				Salt = "ZF/9sBJkTXk=",
				Searchable = true
			});

			context.Users.Add(new User
			{
				FirstName = "Erik",
				Surname = "Engelbreckt",
				BirthDate = DateTime.Parse("1993-04-12"),
				LoginName = "eee",
				EmailAddress = "foo@e.com",
				Password = "e4bf1518d4c9f2ffce856d92e05574c1d8c1c3fd7e3ff1f5313b6a0d04091950",
				Salt = "Heuqth7wfe0=",
				Searchable = true
			});

			context.Users.Add(new User
			{
				FirstName = "Felicia",
				Surname = "Fridolin",
				BirthDate = DateTime.Parse("1993-04-12"),
				LoginName = "fff",
				EmailAddress = "foo@f.com",
				Password = "0cb08ebc167809e2e7a13b82ebb6dd8c3ef8e6e1b141d58626b484e536e3e185",
				Salt = "HEHK562SgPA=",
				Searchable = true
			});

			context.Users.Add(new User
			{
				FirstName = "Gunnar",
				Surname = "Göransson",
				BirthDate = DateTime.Parse("1993-04-12"),
				LoginName = "ggg",
				EmailAddress = "foo@g.com",
				Password = "663f5ca495b19048557a5aaa1cb8bcd005a3b0efee1234a83cf415cc44d15521",
				Salt = "mb7r+n3KVCc=",
				Searchable = false
			});

			context.Users.Add(new User
			{
				FirstName = "Henrik",
				Surname = "Hall",
				BirthDate = DateTime.Parse("1993-04-12"),
				LoginName = "hhh",
				EmailAddress = "foo@h.com",
				Password = "a1f236a80c0f5a30db42f9597c5d58b39d90f5c64afc3d5f815193e6160463f2",
				Salt = "tSQSzGP2BeU=",
				Searchable = true
			});

			context.Users.Add(new User
			{
				FirstName = "Ida",
				Surname = "Iris",
				BirthDate = DateTime.Parse("1993-04-12"),
				LoginName = "iii",
				EmailAddress = "foo@i.com",
				Password = "db442eebc76e6f2d3759a263c93f7eeec2cb100ce644f9fe2cda35910d916fd5",
				Salt = "GTRWhUgbKus=",
				Searchable = true
			});

			context.Users.Add(new User
			{
				FirstName = "Johanna",
				Surname = "Juholt",
				BirthDate = DateTime.Parse("1993-04-12"),
				LoginName = "jjj",
				EmailAddress = "foo@j.com",
				Password = "da9bc2ae249a65e3f2dfa8c89d6ed33c2444c1b8f1336d26100aae7d829c4dac",
				Salt = "HuutonZ4MWM=",
				Searchable = true
			});

			context.Users.Add(new User
			{
				FirstName = "Karl",
				Surname = "Karlsson",
				BirthDate = DateTime.Parse("1993-04-12"),
				LoginName = "kkk",
				EmailAddress = "foo@k.com",
				Password = "053d01ea097abda77a1d80cc25bad736cc7c75461387cdaf5a3af88254f0e49e",
				Salt = "mt7cB9tG130=",
				Searchable = false
			});

			context.SaveChanges();
		}

		private void SeedMessages(DateMateContext context)
		{
			context.Messages.Add(new Message { UserId = 3, Text = "Gobbledygook.", TimeStamp = DateTime.Now, ToId = 2 });
			context.Messages.Add(new Message { UserId = 1, Text = "Gobbledygook.", TimeStamp = DateTime.Now, ToId = 2 });
			context.Messages.Add(new Message { UserId = 3, Text = "Gobbledygook.", TimeStamp = DateTime.Now, ToId = 1 });
			context.Messages.Add(new Message { UserId = 2, Text = "Gobbledygook.", TimeStamp = DateTime.Now, ToId = 5 });
			context.Messages.Add(new Message { UserId = 4, Text = "Sampha palapa!", TimeStamp = DateTime.Now, ToId = 3 });
			context.Messages.Add(new Message { UserId = 3, Text = "Sampha palapa!", TimeStamp = DateTime.Now, ToId = 4 });
			context.Messages.Add(new Message { UserId = 1, Text = "Sampha palapa!", TimeStamp = DateTime.Now, ToId = 5 });
			context.Messages.Add(new Message { UserId = 1, Text = "Hey, hey, ningen sucker.", TimeStamp = DateTime.Now, ToId = 2 });
			context.Messages.Add(new Message { UserId = 2, Text = "You can't see California without Marlon Brando's eyes.", TimeStamp = DateTime.Now, ToId = 1 });
			context.Messages.Add(new Message { UserId = 4, Text = "Why hello there. ;)", TimeStamp = DateTime.Now, ToId = 3 });
			context.Messages.Add(new Message { UserId = 4, Text = "Why hello there. ;)", TimeStamp = DateTime.Now, ToId = 1 });
			context.Messages.Add(new Message { UserId = 2, Text = "'Sup bby boy!", TimeStamp = DateTime.Now, ToId = 4 });
			context.Messages.Add(new Message { UserId = 5, Text = "Hi bae.", TimeStamp = DateTime.Now, ToId = 2 });

			context.SaveChanges();
		}

		private void SeedFriendships(DateMateContext context)
		{
			context.Friendships.Add(new Friendship { RequestById = 1, RequestToId = 2, Status = FriendshipRequestStatus.Confirmed });
			context.Friendships.Add(new Friendship { RequestById = 3, RequestToId = 1, Status = FriendshipRequestStatus.Pending });
			context.Friendships.Add(new Friendship { RequestById = 4, RequestToId = 1, Status = FriendshipRequestStatus.Confirmed });
			context.Friendships.Add(new Friendship { RequestById = 4, RequestToId = 2, Status = FriendshipRequestStatus.Confirmed });
			context.Friendships.Add(new Friendship { RequestById = 2, RequestToId = 3, Status = FriendshipRequestStatus.Confirmed });
			context.Friendships.Add(new Friendship { RequestById = 1, RequestToId = 5, Status = FriendshipRequestStatus.Pending });
			context.Friendships.Add(new Friendship { RequestById = 6, RequestToId = 1, Status = FriendshipRequestStatus.Pending });
			context.Friendships.Add(new Friendship { RequestById = 1, RequestToId = 7, Status = FriendshipRequestStatus.Confirmed });
			context.Friendships.Add(new Friendship { RequestById = 8, RequestToId = 1, Status = FriendshipRequestStatus.Confirmed });

			context.SaveChanges();
		}

		private void SeedPictures(DateMateContext context)
		{
			context.Pictures.Add(new Picture { Id = 5, FileName = "5c2e9a2a-74a4-43de-90f3-5c3600eb99b1.jpg" });
			context.Pictures.Add(new Picture { Id = 1, FileName = "35d6440f-2934-4023-ae98-388d0a5c4dec.jpg" });

			context.SaveChanges();
		}
	}
}
