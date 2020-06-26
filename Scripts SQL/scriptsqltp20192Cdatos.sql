USE [TP-20192C];
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (1, N'Fraude')
GO
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (2, N'Indebida')
GO
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (3, N'Violacion de derechos')
GO
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (4, N'Contiene informacion politica')
GO
INSERT INTO [dbo].[Usuarios]
           ([Nombre]
           ,[Apellido]
           ,[FechaNacimiento]
           ,[UserName]
           ,[Email]
           ,[Password]
           ,[Foto]
           ,[TipoUsuario]
           ,[FechaCracion]
           ,[Activo]
           ,[Token])
     VALUES
           ('MARCOS'
           ,'PEREZ'
           ,'01/02/1978'
           ,'admin'
           ,'admin@test.com'
           ,'12121212'
           ,''
           ,1
           ,getdate()
           ,1
           ,'19E41C31E74A4526')
GO

/****** Inserto datos en usuario y propuestas  ******/
INSERT INTO Usuarios values ('NOMBRE1','APELLIDO1','19800816','NOMBRE1.APELLIDO1','emailnombre1@gmail.com',123456,null,2,getdate(),1,'ESTEESMITOKEN1'),
('NOMBRE2','APELLIDO2','19800817','NOMBRE2.APELLIDO2','emailnombre2@gmail.com',123456,null,2,'20191018',1,'ESTEESMITOKEN2'),
('NOMBRE3','APELLIDO3','19800818','NOMBRE3.APELLIDO3','emailnombre3@gmail.com',123456,null,2,'20191018',1,'ESTEESMITOKEN3'),
('NOMBRE4','APELLIDO4','19800819','NOMBRE4.APELLIDO4','emailnombre4@gmail.com',123456,null,2,'20191018',1,'ESTEESMITOKEN4')
GO

select * from Propuestas
INSERT INTO Propuestas values('Nombre propuesta 1',
't is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using Content here, content here, making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text',
'20191017','20191224','1112333333',1,'',2,0,8)
GO

INSERT INTO PropuestasDonacionesMonetarias values (1,300000,'552543245432')
GO

INSERT INTO Propuestas values('Nombre propuesta 2',
't is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using Content here, content here, making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text',
'20191017','20191224','1112312121',1,'',1,0,6)
GO

INSERT INTO PropuestasDonacionesMonetarias values (2,34575,'132132123321')
GO

INSERT INTO Propuestas values('Nombre propuesta 3',
't is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using Content here, content here, making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text',
'20191017','20191224','112457545',2,'',3,0,7)
GO

INSERT INTO PropuestasDonacionesInsumos values (3,'object',20)
GO

INSERT INTO Propuestas values('Nombre propuesta 4',
't is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using Content here, content here, making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text',
'20191017','20191224','1112345678',3,'',4,0,9)
GO

INSERT INTO PropuestasDonacionesHorasTrabajo values(4,36,'Carpintero')
GO

INSERT INTO Propuestas values('Nombre propuesta 5',
't is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using Content here, content here, making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text',
'20191017','20191224','123575554',2,'',1,0,5)
GO

INSERT INTO PropuestasDonacionesInsumos values (5,'object2',1000)
GO
