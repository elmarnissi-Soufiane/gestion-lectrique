use master
go

create database db_ProjectEle
go

use db_ProjectEle
go


IF (OBJECT_ID('Utilisateur') IS NULL)
BEGIN
	CREATE TABLE Utilisateur
	(
		Id INT NOT NULL IDENTITY PRIMARY KEY,
		username NVARCHAR(99) NOT NULL UNIQUE,
		PasswordL NVARCHAR(99) NOT NULL
	)
END
ELSE
	PRINT 'la table Utilisateur deja exists'
GO

IF (OBJECT_ID('Pylones') IS NULL)
BEGIN
  CREATE TABLE Pylones (
    
    NumeroPylone INT NOT NULL PRIMARY KEY,
    LigneElectrique VARCHAR(10) NOT NULL CHECK(LigneElectrique IN ('60KV', '225KV', '400KV')),
    Ville VARCHAR(100) NOT NULL,
    Longitude DECIMAL(9,6) NOT NULL,
    Latitude DECIMAL(9,6) NOT NULL,
    EtatDegradation VARCHAR(10) NOT NULL CHECK(EtatDegradation IN ('Bon', 'Dégradé'))
  )
END
ELSE
  PRINT 'La table Pylones existe déjà'
GO

IF (OBJECT_ID('Ouvriers') IS NULL)
BEGIN
  CREATE TABLE Ouvriers (
    
    CIN VARCHAR(10) NOT NULL  PRIMARY KEY,
    NomComplet VARCHAR(100) NOT NULL,
    Ville VARCHAR(100) NOT NULL,
    fonction VARCHAR(100) NOT NULL CHECK(fonction IN ('Chauffeur', 'Peintre', 'Chef d’équipe', 'magasiniers')),
    Telephone VARCHAR(15) NOT NULL,
    DateNaissance DATE,
    DateDebutActivite DATE 
  )
END
ELSE
  PRINT 'La table Ouvriers existe déjà'
GO

IF (OBJECT_ID('Travaux') IS NULL)
BEGIN
  CREATE TABLE Travaux (
    ID_Travaux INT PRIMARY KEY,
    NumeroPylone INT NOT NULL,
    CIN VARCHAR(10) NOT NULL,
    DateTravail DATE ,
    TauxAvancement DECIMAL(3,2) NOT NULL CHECK(TauxAvancement IN (0.5, 0.25, 0.125)) ,
    FOREIGN KEY (NumeroPylone) REFERENCES Pylones(NumeroPylone) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (CIN) REFERENCES Ouvriers(CIN) ON DELETE CASCADE ON UPDATE CASCADE
  )
END
ELSE
  PRINT 'La table Travaux existe déjà'
GO

IF (OBJECT_ID('Paiements') IS NULL)
BEGIN
  CREATE TABLE Paiements (
    ID_Paiements INT PRIMARY KEY,
    CIN VARCHAR(10) NOT NULL,
    Mois DATE,
    Montant DECIMAL(8,2) NOT NULL,
    FOREIGN KEY (CIN) REFERENCES Ouvriers(CIN) ON DELETE CASCADE ON UPDATE CASCADE
  )
END
ELSE
  PRINT 'La table Paiements existe déjà'
GO

IF (OBJECT_ID('Vehicules') IS NULL)
BEGIN
  CREATE TABLE Vehicules (
   
    Immatricule VARCHAR(10) NOT NULL PRIMARY KEY,
    Model VARCHAR(100) NOT NULL,
    TypeCarburant VARCHAR(10) NOT NULL CHECK(TypeCarburant IN ('Essence', 'Diesel')),
    KilometrageInitial INT NOT NULL
  )
END
ELSE
  PRINT 'La table Vehicules existe déjà'
GO

IF (OBJECT_ID('ConsommationCarburant') IS NULL)
BEGIN
  CREATE TABLE ConsommationCarburant (
    ID_ConsommationCarburant INT PRIMARY KEY,
    Immatricule VARCHAR(10) NOT NULL,
    VolumeGasoil DECIMAL(8,2) NOT NULL,
    PrixBon DECIMAL(8,2) NOT NULL,
    DateRemplissage DATE,
    Kilometrage INT NOT NULL,
    FOREIGN KEY (Immatricule) REFERENCES Vehicules(Immatricule) ON DELETE CASCADE ON UPDATE CASCADE
  )
END
ELSE
  PRINT 'La table ConsommationCarburant existe déjà'
GO

IF (OBJECT_ID('Repos') IS NULL)
BEGIN
  CREATE TABLE Repos (
    ID_Repos INT PRIMARY KEY NOT NULL,
    CIN VARCHAR(10) NOT NULL,
    DateRepos DATE NOT NULL,
    MotifRepos VARCHAR(100) NOT NULL,
    FOREIGN KEY (CIN) REFERENCES Ouvriers(CIN) ON DELETE CASCADE ON UPDATE CASCADE

)
END
ELSE
  PRINT 'La table Repos existe déjà'
GO

select * from Pylones
select * from Paiements
select * from Ouvriers
select * from Vehicules
select * from ConsommationCarburant
select * from Repos
select * from Travaux

insert into Travaux values (1, 1, 'PA152591', '07/05/2023', 0.25)

insert into Utilisateur values (1, 'soufiane.elmarnissi', 'Ss123456')