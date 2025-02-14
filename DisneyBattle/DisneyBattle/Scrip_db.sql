CREATE DATABASE DisneyBattle;
GO
USE [DisneyBattle]
GO
/****** Object:  Table [dbo].[utilisateurs]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[utilisateurs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pseudo] [nvarchar](50) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[mot_de_passe] [nvarchar](255) NOT NULL,
	[date_inscription] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[equipes]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[equipes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nom] [nvarchar](100) NOT NULL,
	[utilisateur_id] [int] NOT NULL,
	[date_creation] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[combats]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[combats](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[equipe1_id] [int] NOT NULL,
	[equipe2_id] [int] NOT NULL,
	[equipe_gagnante_id] [int] NULL,
	[experience_gagnee] [int] NOT NULL,
	[date_combat] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[palmares_utilisateurs]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Vue pour le palmarès des utilisateurs
CREATE VIEW [dbo].[palmares_utilisateurs]
AS
SELECT 
    u.id,
    u.pseudo,
    COUNT(DISTINCT c.id) as total_combats,
    COUNT(DISTINCT CASE WHEN c.equipe_gagnante_id = e.id THEN c.id END) as victoires,
    COUNT(DISTINCT CASE WHEN c.equipe_gagnante_id != e.id AND c.equipe_gagnante_id IS NOT NULL THEN c.id END) as defaites,
    CAST(CAST(COUNT(DISTINCT CASE WHEN c.equipe_gagnante_id = e.id THEN c.id END) AS FLOAT) * 100.0 / 
         NULLIF(COUNT(DISTINCT c.id), 0) AS DECIMAL(5,2)) as pourcentage_victoire
FROM 
    utilisateurs u
    LEFT JOIN equipes e ON u.id = e.utilisateur_id
    LEFT JOIN combats c ON (c.equipe1_id = e.id OR c.equipe2_id = e.id)
GROUP BY 
    u.id, u.pseudo;

GO
/****** Object:  Table [dbo].[combats_objets]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[combats_objets](
	[combat_id] [int] NOT NULL,
	[personnage_id] [int] NOT NULL,
	[objet_id] [int] NOT NULL,
	[tour] [int] NOT NULL,
 CONSTRAINT [PK_combats_objets] PRIMARY KEY CLUSTERED 
(
	[combat_id] ASC,
	[personnage_id] ASC,
	[objet_id] ASC,
	[tour] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[equipes_personnages]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[equipes_personnages](
	[equipe_id] [int] NOT NULL,
	[personnage_id] [int] NOT NULL,
	[position] [int] NULL,
 CONSTRAINT [PK_equipes_personnages] PRIMARY KEY CLUSTERED 
(
	[equipe_id] ASC,
	[personnage_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[historique_niveaux]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[historique_niveaux](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[personnage_id] [int] NOT NULL,
	[combat_id] [int] NOT NULL,
	[ancien_niveau] [int] NOT NULL,
	[nouveau_niveau] [int] NOT NULL,
	[date_changement] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lieux]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lieux](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nom] [nvarchar](100) NOT NULL,
	[description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[objets]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[objets](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nom] [nvarchar](100) NOT NULL,
	[description] [nvarchar](max) NULL,
	[niveau_requis] [int] NOT NULL,
	[bonus_pv] [int] NULL,
	[bonus_attaque] [int] NULL,
	[bonus_defense] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[personnages]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[personnages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nom] [nvarchar](100) NOT NULL,
	[alignement_id] [int] NOT NULL,
	[niveau] [int] NOT NULL,
	[experience] [int] NOT NULL,
	[points_vie] [int] NOT NULL,
	[points_attaque] [int] NOT NULL,
	[points_defense] [int] NOT NULL,
	[lieu_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[personnages_objets_autorises]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[personnages_objets_autorises](
	[personnage_id] [int] NOT NULL,
	[objet_id] [int] NOT NULL,
 CONSTRAINT [PK_personnages_objets] PRIMARY KEY CLUSTERED 
(
	[personnage_id] ASC,
	[objet_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[personnages_points_faibles]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[personnages_points_faibles](
	[personnage_id] [int] NOT NULL,
	[point_faible_id] [int] NOT NULL,
 CONSTRAINT [PK_personnages_points_faibles] PRIMARY KEY CLUSTERED 
(
	[personnage_id] ASC,
	[point_faible_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[points_faibles]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[points_faibles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nom] [nvarchar](100) NOT NULL,
	[description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[typePersonnage]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[typePersonnage](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nom] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[lieux] ON 
GO
INSERT [dbo].[lieux] ([id], [nom], [description]) VALUES (1, N'Château de la Belle au Bois Dormant', N'Un magnifique château enchanté au cœur de la forêt')
GO
INSERT [dbo].[lieux] ([id], [nom], [description]) VALUES (2, N'Agrabah', N'Une ville mystérieuse au milieu du désert')
GO
INSERT [dbo].[lieux] ([id], [nom], [description]) VALUES (3, N'Atlantica', N'Le royaume sous-marin du Roi Triton')
GO
INSERT [dbo].[lieux] ([id], [nom], [description]) VALUES (4, N'Pays Imaginaire', N'Une île magique où les enfants ne grandissent jamais')
GO
INSERT [dbo].[lieux] ([id], [nom], [description]) VALUES (5, N'Ville d''Halloween', N'Une ville sombre et effrayante dédiée à Halloween')
GO
INSERT [dbo].[lieux] ([id], [nom], [description]) VALUES (6, N'La Forêt des Nains', N'Une forêt paisible où vivent les sept nains')
GO
INSERT [dbo].[lieux] ([id], [nom], [description]) VALUES (7, N'Le Royaume de Corona', N'Un royaume prospère entouré par la mer')
GO
INSERT [dbo].[lieux] ([id], [nom], [description]) VALUES (8, N'La Terre des Lions', N'Les vastes plaines africaines')
GO
SET IDENTITY_INSERT [dbo].[lieux] OFF
GO
SET IDENTITY_INSERT [dbo].[objets] ON 
GO
INSERT [dbo].[objets] ([id], [nom], [description], [niveau_requis], [bonus_pv], [bonus_attaque], [bonus_defense]) VALUES (1, N'Épée du Prince', N'Une épée enchantée qui brille dans les ténèbres', 1, 0, 15, 5)
GO
INSERT [dbo].[objets] ([id], [nom], [description], [niveau_requis], [bonus_pv], [bonus_attaque], [bonus_defense]) VALUES (2, N'Lampe Magique', N'Contient un génie qui accorde des souhaits', 3, 10, 20, 10)
GO
INSERT [dbo].[objets] ([id], [nom], [description], [niveau_requis], [bonus_pv], [bonus_attaque], [bonus_defense]) VALUES (3, N'Pomme Empoisonnée', N'Une pomme à l''apparence délicieuse mais mortelle', 2, -20, 0, 0)
GO
INSERT [dbo].[objets] ([id], [nom], [description], [niveau_requis], [bonus_pv], [bonus_attaque], [bonus_defense]) VALUES (4, N'Trident de Triton', N'Le puissant trident du roi des mers', 4, 0, 25, 15)
GO
INSERT [dbo].[objets] ([id], [nom], [description], [niveau_requis], [bonus_pv], [bonus_attaque], [bonus_defense]) VALUES (5, N'Miroir Magique', N'Révèle la vérité et peut aveugler les ennemis', 2, 0, 10, 5)
GO
INSERT [dbo].[objets] ([id], [nom], [description], [niveau_requis], [bonus_pv], [bonus_attaque], [bonus_defense]) VALUES (6, N'Dague du Ténébreux', N'Une dague maudite aux pouvoirs obscurs', 5, -10, 30, 0)
GO
INSERT [dbo].[objets] ([id], [nom], [description], [niveau_requis], [bonus_pv], [bonus_attaque], [bonus_defense]) VALUES (7, N'Couronne de Raiponce', N'La couronne perdue de Corona', 3, 0, 5, 15)
GO
INSERT [dbo].[objets] ([id], [nom], [description], [niveau_requis], [bonus_pv], [bonus_attaque], [bonus_defense]) VALUES (8, N'Crochet Empoisonné', N'Le crochet du Capitaine porteur de poison', 4, 0, 20, 5)
GO
INSERT [dbo].[objets] ([id], [nom], [description], [niveau_requis], [bonus_pv], [bonus_attaque], [bonus_defense]) VALUES (9, N'Rose Enchantée', N'Une rose magique qui donne force et protection', 2, 15, 5, 10)
GO
INSERT [dbo].[objets] ([id], [nom], [description], [niveau_requis], [bonus_pv], [bonus_attaque], [bonus_defense]) VALUES (10, N'Médaillon d''Ursula', N'Contient la voix volée d''une sirène', 3, 0, 15, 5)
GO
SET IDENTITY_INSERT [dbo].[objets] OFF
GO
SET IDENTITY_INSERT [dbo].[personnages] ON 
GO
INSERT [dbo].[personnages] ([id], [nom], [alignement_id], [niveau], [experience], [points_vie], [points_attaque], [points_defense], [lieu_id]) VALUES (1, N'Belle', 1, 1, 0, 100, 60, 70, 1)
GO
INSERT [dbo].[personnages] ([id], [nom], [alignement_id], [niveau], [experience], [points_vie], [points_attaque], [points_defense], [lieu_id]) VALUES (2, N'Aladdin', 1, 1, 0, 110, 75, 65, 2)
GO
INSERT [dbo].[personnages] ([id], [nom], [alignement_id], [niveau], [experience], [points_vie], [points_attaque], [points_defense], [lieu_id]) VALUES (3, N'Ariel', 1, 1, 0, 95, 65, 60, 3)
GO
INSERT [dbo].[personnages] ([id], [nom], [alignement_id], [niveau], [experience], [points_vie], [points_attaque], [points_defense], [lieu_id]) VALUES (4, N'Peter Pan', 1, 1, 0, 105, 70, 65, 4)
GO
INSERT [dbo].[personnages] ([id], [nom], [alignement_id], [niveau], [experience], [points_vie], [points_attaque], [points_defense], [lieu_id]) VALUES (5, N'Raiponce', 1, 1, 0, 100, 65, 70, 7)
GO
INSERT [dbo].[personnages] ([id], [nom], [alignement_id], [niveau], [experience], [points_vie], [points_attaque], [points_defense], [lieu_id]) VALUES (6, N'Simba', 1, 1, 0, 120, 80, 75, 8)
GO
INSERT [dbo].[personnages] ([id], [nom], [alignement_id], [niveau], [experience], [points_vie], [points_attaque], [points_defense], [lieu_id]) VALUES (7, N'Maléfique', 2, 1, 0, 130, 90, 85, 1)
GO
INSERT [dbo].[personnages] ([id], [nom], [alignement_id], [niveau], [experience], [points_vie], [points_attaque], [points_defense], [lieu_id]) VALUES (8, N'Jafar', 2, 1, 0, 125, 85, 80, 2)
GO
INSERT [dbo].[personnages] ([id], [nom], [alignement_id], [niveau], [experience], [points_vie], [points_attaque], [points_defense], [lieu_id]) VALUES (9, N'Ursula', 2, 1, 0, 120, 80, 75, 3)
GO
INSERT [dbo].[personnages] ([id], [nom], [alignement_id], [niveau], [experience], [points_vie], [points_attaque], [points_defense], [lieu_id]) VALUES (10, N'Capitaine Crochet', 2, 1, 0, 115, 75, 70, 4)
GO
INSERT [dbo].[personnages] ([id], [nom], [alignement_id], [niveau], [experience], [points_vie], [points_attaque], [points_defense], [lieu_id]) VALUES (11, N'Mère Gothel', 2, 1, 0, 110, 70, 75, 7)
GO
INSERT [dbo].[personnages] ([id], [nom], [alignement_id], [niveau], [experience], [points_vie], [points_attaque], [points_defense], [lieu_id]) VALUES (12, N'Scar', 2, 1, 0, 125, 85, 70, 8)
GO
SET IDENTITY_INSERT [dbo].[personnages] OFF
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (1, 3)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (1, 5)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (1, 9)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (2, 1)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (2, 2)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (3, 4)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (3, 10)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (4, 1)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (4, 8)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (5, 7)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (5, 9)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (7, 3)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (7, 6)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (8, 2)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (8, 6)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (9, 4)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (9, 10)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (10, 6)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (10, 8)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (11, 3)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (11, 7)
GO
INSERT [dbo].[personnages_objets_autorises] ([personnage_id], [objet_id]) VALUES (12, 6)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (1, 2)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (2, 1)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (3, 3)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (4, 1)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (5, 2)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (6, 1)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (7, 5)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (7, 6)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (8, 1)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (8, 7)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (9, 3)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (9, 7)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (10, 1)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (10, 3)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (11, 2)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (11, 8)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (12, 1)
GO
INSERT [dbo].[personnages_points_faibles] ([personnage_id], [point_faible_id]) VALUES (12, 8)
GO
SET IDENTITY_INSERT [dbo].[points_faibles] ON 
GO
INSERT [dbo].[points_faibles] ([id], [nom], [description]) VALUES (1, N'Orgueil', N'Un excès de confiance qui mène à la perte')
GO
INSERT [dbo].[points_faibles] ([id], [nom], [description]) VALUES (2, N'Amour', N'L''amour véritable peut briser n''importe quel sortilège')
GO
INSERT [dbo].[points_faibles] ([id], [nom], [description]) VALUES (3, N'Eau', N'Faiblesse aux attaques de type eau')
GO
INSERT [dbo].[points_faibles] ([id], [nom], [description]) VALUES (4, N'Feu', N'Vulnérabilité aux attaques de feu')
GO
INSERT [dbo].[points_faibles] ([id], [nom], [description]) VALUES (5, N'Lumière', N'Les ténèbres ne peuvent résister à la lumière')
GO
INSERT [dbo].[points_faibles] ([id], [nom], [description]) VALUES (6, N'Magie blanche', N'Faiblesse contre la magie pure')
GO
INSERT [dbo].[points_faibles] ([id], [nom], [description]) VALUES (7, N'Objets maudits', N'Dépendance à un objet source de pouvoir')
GO
INSERT [dbo].[points_faibles] ([id], [nom], [description]) VALUES (8, N'Sacrifice', N'Le sacrifice par amour peut tout vaincre')
GO
SET IDENTITY_INSERT [dbo].[points_faibles] OFF
GO
SET IDENTITY_INSERT [dbo].[typePersonnage] ON 
GO
INSERT [dbo].[typePersonnage] ([id], [nom]) VALUES (1, N'Héros')
GO
INSERT [dbo].[typePersonnage] ([id], [nom]) VALUES (2, N'Vilain')
GO
SET IDENTITY_INSERT [dbo].[typePersonnage] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__personna__DF90DC2C89D959C4]    Script Date: 14-02-25 10:51:20 ******/
ALTER TABLE [dbo].[personnages] ADD UNIQUE NONCLUSTERED 
(
	[nom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__aligneme__DF90DC2C3FA1DF58]    Script Date: 14-02-25 10:51:20 ******/
ALTER TABLE [dbo].[typePersonnage] ADD UNIQUE NONCLUSTERED 
(
	[nom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__utilisat__AB6E61649A1F478E]    Script Date: 14-02-25 10:51:20 ******/
ALTER TABLE [dbo].[utilisateurs] ADD UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__utilisat__EA0EEA2298E82E82]    Script Date: 14-02-25 10:51:20 ******/
ALTER TABLE [dbo].[utilisateurs] ADD UNIQUE NONCLUSTERED 
(
	[pseudo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[combats] ADD  DEFAULT ((100)) FOR [experience_gagnee]
GO
ALTER TABLE [dbo].[combats] ADD  DEFAULT (getdate()) FOR [date_combat]
GO
ALTER TABLE [dbo].[equipes] ADD  DEFAULT (getdate()) FOR [date_creation]
GO
ALTER TABLE [dbo].[historique_niveaux] ADD  DEFAULT (getdate()) FOR [date_changement]
GO
ALTER TABLE [dbo].[objets] ADD  DEFAULT ((1)) FOR [niveau_requis]
GO
ALTER TABLE [dbo].[objets] ADD  DEFAULT ((0)) FOR [bonus_pv]
GO
ALTER TABLE [dbo].[objets] ADD  DEFAULT ((0)) FOR [bonus_attaque]
GO
ALTER TABLE [dbo].[objets] ADD  DEFAULT ((0)) FOR [bonus_defense]
GO
ALTER TABLE [dbo].[personnages] ADD  DEFAULT ((1)) FOR [niveau]
GO
ALTER TABLE [dbo].[personnages] ADD  DEFAULT ((0)) FOR [experience]
GO
ALTER TABLE [dbo].[utilisateurs] ADD  DEFAULT (getdate()) FOR [date_inscription]
GO
ALTER TABLE [dbo].[combats]  WITH CHECK ADD  CONSTRAINT [FK_combats_equipe1] FOREIGN KEY([equipe1_id])
REFERENCES [dbo].[equipes] ([id])
GO
ALTER TABLE [dbo].[combats] CHECK CONSTRAINT [FK_combats_equipe1]
GO
ALTER TABLE [dbo].[combats]  WITH CHECK ADD  CONSTRAINT [FK_combats_equipe2] FOREIGN KEY([equipe2_id])
REFERENCES [dbo].[equipes] ([id])
GO
ALTER TABLE [dbo].[combats] CHECK CONSTRAINT [FK_combats_equipe2]
GO
ALTER TABLE [dbo].[combats]  WITH CHECK ADD  CONSTRAINT [FK_combats_gagnant] FOREIGN KEY([equipe_gagnante_id])
REFERENCES [dbo].[equipes] ([id])
GO
ALTER TABLE [dbo].[combats] CHECK CONSTRAINT [FK_combats_gagnant]
GO
ALTER TABLE [dbo].[combats_objets]  WITH CHECK ADD  CONSTRAINT [FK_combats_objets_combats] FOREIGN KEY([combat_id])
REFERENCES [dbo].[combats] ([id])
GO
ALTER TABLE [dbo].[combats_objets] CHECK CONSTRAINT [FK_combats_objets_combats]
GO
ALTER TABLE [dbo].[combats_objets]  WITH CHECK ADD  CONSTRAINT [FK_combats_objets_objets] FOREIGN KEY([objet_id])
REFERENCES [dbo].[objets] ([id])
GO
ALTER TABLE [dbo].[combats_objets] CHECK CONSTRAINT [FK_combats_objets_objets]
GO
ALTER TABLE [dbo].[combats_objets]  WITH CHECK ADD  CONSTRAINT [FK_combats_objets_personnages] FOREIGN KEY([personnage_id])
REFERENCES [dbo].[personnages] ([id])
GO
ALTER TABLE [dbo].[combats_objets] CHECK CONSTRAINT [FK_combats_objets_personnages]
GO
ALTER TABLE [dbo].[equipes]  WITH CHECK ADD  CONSTRAINT [FK_equipes_utilisateurs] FOREIGN KEY([utilisateur_id])
REFERENCES [dbo].[utilisateurs] ([id])
GO
ALTER TABLE [dbo].[equipes] CHECK CONSTRAINT [FK_equipes_utilisateurs]
GO
ALTER TABLE [dbo].[equipes_personnages]  WITH CHECK ADD  CONSTRAINT [FK_equipes_personnages_equipes] FOREIGN KEY([equipe_id])
REFERENCES [dbo].[equipes] ([id])
GO
ALTER TABLE [dbo].[equipes_personnages] CHECK CONSTRAINT [FK_equipes_personnages_equipes]
GO
ALTER TABLE [dbo].[equipes_personnages]  WITH CHECK ADD  CONSTRAINT [FK_equipes_personnages_personnages] FOREIGN KEY([personnage_id])
REFERENCES [dbo].[personnages] ([id])
GO
ALTER TABLE [dbo].[equipes_personnages] CHECK CONSTRAINT [FK_equipes_personnages_personnages]
GO
ALTER TABLE [dbo].[historique_niveaux]  WITH CHECK ADD  CONSTRAINT [FK_historique_combats] FOREIGN KEY([combat_id])
REFERENCES [dbo].[combats] ([id])
GO
ALTER TABLE [dbo].[historique_niveaux] CHECK CONSTRAINT [FK_historique_combats]
GO
ALTER TABLE [dbo].[historique_niveaux]  WITH CHECK ADD  CONSTRAINT [FK_historique_personnages] FOREIGN KEY([personnage_id])
REFERENCES [dbo].[personnages] ([id])
GO
ALTER TABLE [dbo].[historique_niveaux] CHECK CONSTRAINT [FK_historique_personnages]
GO
ALTER TABLE [dbo].[personnages]  WITH CHECK ADD  CONSTRAINT [FK_personnages_alignements] FOREIGN KEY([alignement_id])
REFERENCES [dbo].[typePersonnage] ([id])
GO
ALTER TABLE [dbo].[personnages] CHECK CONSTRAINT [FK_personnages_alignements]
GO
ALTER TABLE [dbo].[personnages]  WITH CHECK ADD  CONSTRAINT [FK_personnages_lieux] FOREIGN KEY([lieu_id])
REFERENCES [dbo].[lieux] ([id])
GO
ALTER TABLE [dbo].[personnages] CHECK CONSTRAINT [FK_personnages_lieux]
GO
ALTER TABLE [dbo].[personnages_objets_autorises]  WITH CHECK ADD  CONSTRAINT [FK_objets_autorises_objets] FOREIGN KEY([objet_id])
REFERENCES [dbo].[objets] ([id])
GO
ALTER TABLE [dbo].[personnages_objets_autorises] CHECK CONSTRAINT [FK_objets_autorises_objets]
GO
ALTER TABLE [dbo].[personnages_objets_autorises]  WITH CHECK ADD  CONSTRAINT [FK_objets_autorises_personnages] FOREIGN KEY([personnage_id])
REFERENCES [dbo].[personnages] ([id])
GO
ALTER TABLE [dbo].[personnages_objets_autorises] CHECK CONSTRAINT [FK_objets_autorises_personnages]
GO
ALTER TABLE [dbo].[personnages_points_faibles]  WITH CHECK ADD  CONSTRAINT [FK_points_faibles_personnages] FOREIGN KEY([personnage_id])
REFERENCES [dbo].[personnages] ([id])
GO
ALTER TABLE [dbo].[personnages_points_faibles] CHECK CONSTRAINT [FK_points_faibles_personnages]
GO
ALTER TABLE [dbo].[personnages_points_faibles]  WITH CHECK ADD  CONSTRAINT [FK_points_faibles_points] FOREIGN KEY([point_faible_id])
REFERENCES [dbo].[points_faibles] ([id])
GO
ALTER TABLE [dbo].[personnages_points_faibles] CHECK CONSTRAINT [FK_points_faibles_points]
GO
ALTER TABLE [dbo].[equipes_personnages]  WITH CHECK ADD  CONSTRAINT [CHK_position] CHECK  (([position]>=(1) AND [position]<=(5)))
GO
ALTER TABLE [dbo].[equipes_personnages] CHECK CONSTRAINT [CHK_position]
GO
/****** Object:  StoredProcedure [dbo].[sp_augmenter_experience_apres_combat]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procédure stockée pour augmenter l'expérience après un combat
CREATE PROCEDURE [dbo].[sp_augmenter_experience_apres_combat]
    @combat_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @exp_gagnee INT;
    
    -- Récupérer l'expérience gagnée pour ce combat
    SELECT @exp_gagnee = experience_gagnee 
    FROM combats 
    WHERE id = @combat_id;
    
    -- Mettre à jour l'expérience et le niveau des personnages
    ;WITH PersonnagesAMettreAJour AS (
        SELECT 
            p.id,
            p.experience + @exp_gagnee as nouvelle_experience,
            p.niveau as ancien_niveau,
            FLOOR((p.experience + @exp_gagnee) / 1000) + 1 as nouveau_niveau
        FROM personnages p
        JOIN equipes_personnages ep ON p.id = ep.personnage_id
        JOIN equipes e ON ep.equipe_id = e.id
        JOIN combats c ON c.equipe_gagnante_id = e.id
        WHERE c.id = @combat_id
    )
    UPDATE p
    SET 
        p.experience = paj.nouvelle_experience,
        p.niveau = paj.nouveau_niveau
    FROM personnages p
    JOIN PersonnagesAMettreAJour paj ON p.id = paj.id;
    
    -- Enregistrer les changements de niveau
    INSERT INTO historique_niveaux (personnage_id, combat_id, ancien_niveau, nouveau_niveau)
    SELECT 
        id, @combat_id, ancien_niveau, nouveau_niveau
    FROM PersonnagesAMettreAJour
    WHERE ancien_niveau != nouveau_niveau;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_verifier_utilisation_objet]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Création d'une procédure stockée pour vérifier si un objet peut être utilisé
CREATE PROCEDURE [dbo].[sp_verifier_utilisation_objet]
    @personnage_id INT,
    @objet_id INT,
    @peut_utiliser BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 
        FROM personnages p
        JOIN personnages_objets_autorises poa ON p.id = poa.personnage_id
        JOIN objets o ON o.id = poa.objet_id
        WHERE p.id = @personnage_id 
        AND o.id = @objet_id
        AND p.niveau >= o.niveau_requis
    )
        SET @peut_utiliser = 1
    ELSE
        SET @peut_utiliser = 0
END;

GO
/****** Object:  Trigger [dbo].[TR_after_combat_update]    Script Date: 14-02-25 10:51:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Trigger pour la mise à jour après un combat
CREATE TRIGGER [dbo].[TR_after_combat_update]
ON [dbo].[combats]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        JOIN deleted d ON i.id = d.id
        WHERE i.equipe_gagnante_id IS NOT NULL 
        AND d.equipe_gagnante_id IS NULL
    )
    BEGIN
        DECLARE @combat_id INT;
        SELECT @combat_id = id FROM inserted;
        EXEC sp_augmenter_experience_apres_combat @combat_id;
    END
END;

GO
ALTER TABLE [dbo].[combats] ENABLE TRIGGER [TR_after_combat_update]
GO