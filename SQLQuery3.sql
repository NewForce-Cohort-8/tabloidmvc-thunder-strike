SET IDENTITY_INSERT [UserProfile] ON
INSERT INTO [UserProfile] (
[Id], [FirstName], [LastName], [DisplayName], [Email], [CreateDateTime], [ImageLocation], [UserTypeId])
VALUES (4, 'john', 'Dane', 'poopypants', 'user2@email.com', SYSDATETIME(), NULL, 2);
SET IDENTITY_INSERT [UserProfile] OFF