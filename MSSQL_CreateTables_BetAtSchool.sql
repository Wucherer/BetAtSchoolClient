Create TABLE dbo.isAdmin(
	Username Varchar(30) PRIMARY KEY
)


CREATE TABLE Stations(
	Station_ID int PRIMARY KEY NOT NULL,
	Stationname Varchar(40)
)

CREATE TABLE Questions(
	Question_ID int PRIMARY KEY NOT NULL,
	Question_Desc Varchar(50),
	Station_Id int,
	Quote decimal,

	CONSTRAINT fk_QuestionsStations FOREIGN KEY (Station_Id)
		REFERENCES [BetAtSchoolDB].[dbo].[Stations](Station_ID)
)

CREATE TABLE dbo.Answers(
	Answer_ID int PRIMARY KEY NOT NULL,
	Answer_Desc Varchar(50),
	Question_ID int,
	isRichtig int,

	CONSTRAINT fk_AnswersQuestions FOREIGN KEY (Question_ID)
		REFERENCES [BetAtSchoolDB].[dbo].[Questions](Question_ID)
)

CREATE TABLE USERTODT(
	Username Varchar(50) PRIMARY KEY NOT NULL,
	Credit int, 
	Email Varchar(60)
)

INSERT INTO isAdmin VALUES('wuchered');

Insert into Stations values ('0','POS');
Insert into Stations values ('1','Mathe');
Insert into Stations values ('2','Deutsch');
Insert into Stations values ('3','English');
Insert into Stations values ('4','TINF');
Insert into Stations values ('5','NVS');
Insert into Stations values ('6','SYP');
Insert into Stations values ('7','BSPK');
Insert into Stations values ('8','LabView');
Insert into Stations values ('9','Sharepoint');

Insert into Questions values ('0','Welche ist keine Programmiersprache?','0','2');
Insert into Questions values ('1','1+1 = ?','1','1');
Insert into Questions  values ('2','Von wem ist Faust ?','2','2');
Insert into Questions  values ('3','Wer unterrichtet E ?','3','1');
Insert into Questions  values ('4','Wie viele Stellen kennt das Bin-System ?','4','1');
Insert into Questions  values ('5','Wie funktioniert Shell-Script?','5','1');
Insert into Questions  values ('6','Welches ist eine P.entwicklungs-methode?','6','1');
Insert into Questions  values ('7','Wie heisst das beste Spiel in Turnen?','7','1');
Insert into Questions  values ('8','Ist LabView eine Programmiersprache?','8','3');
Insert into Questions  values ('9','Wer entwickelte Sharepoint?','9','1');

Insert into Answers values ('0','C#','0','1');
Insert into Answers values ('1','Java','0','0');
Insert into Answers  values ('2','HTML','0','0');
Insert into Answers  values ('3','Gobi','0','0');
Insert into Answers  values ('4','483','1','0');
Insert into Answers  values ('5','41','1','0');
Insert into Answers  values ('6','3','1','0');
Insert into Answers  values ('7','2','1','1');
Insert into Answers  values ('8','Goethe','2','1');
Insert into Answers  values ('9','Schiller','2','0');
Insert into Answers  values ('10','JKR','2','0');
Insert into Answers  values ('11','Trump','2','0');
Insert into Answers  values ('12','xxx','3','0');
Insert into Answers  values ('13','yyy','3','0');
Insert into Answers  values ('14','zzz','3','0');
Insert into Answers  values ('15','LEE','3','1');
Insert into Answers  values ('16','3','4','0');
Insert into Answers  values ('17','16','4','0');
Insert into Answers  values ('18','2','4','1');
Insert into Answers  values ('19','-1','4','0');
Insert into Answers  values ('20','Gut','5','0');
Insert into Answers  values ('21','Manchmal','5','0');
Insert into Answers  values ('22','Schlecht','5','0');
Insert into Answers  values ('23','Nie','5','1');
Insert into Answers  values ('24','Scrum','6','0');
Insert into Answers  values ('25','Pflaume','6','0');
Insert into Answers  values ('26','Sex','6','0');
Insert into Answers  values ('27','Sex2','6','1');
Insert into Answers  values ('28','Alles','7','1');
Insert into Answers  values ('29','Floorball','7','0');
Insert into Answers  values ('30','Fussball','7','0');
Insert into Answers  values ('31','Tenis','7','0');
Insert into Answers  values ('32','Nein','8','1');
Insert into Answers  values ('33','Ja','8','0');
Insert into Answers  values ('34','kA','8','0');
Insert into Answers  values ('35','Mal so, mal so','8','0');
Insert into Answers  values ('36','xxx','9','0');
Insert into Answers  values ('37','yyy','9','0');
Insert into Answers  values ('38','Prof. Karasek','9','0');
Insert into Answers  values ('39','1Stein','9','1');

Insert into USERTODT values('dave', 100, '');
