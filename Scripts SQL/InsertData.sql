BEGIN
   IF NOT EXISTS (SELECT * FROM [TP-20192C].[dbo].[Usuarios] 
                   WHERE [Nombre] = 'MARCOS')
   BEGIN
       INSERT INTO [TP-20192C].[dbo].[Usuarios]
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
   END
END
-------------------------------------------------------------------------------
BEGIN
   IF NOT EXISTS (SELECT * FROM [TP-20192C].[dbo].[Propuestas] 
                   WHERE [Nombre] = 'Comedor nueva esperanza'
                   AND [TelefonoContacto] = '11556456789')
   BEGIN
       INSERT INTO [TP-20192C].[dbo].[Propuestas]
           ([Nombre]
           ,[Descripcion]
           ,[FechaCreacion]
           ,[FechaFin]
           ,[TelefonoContacto]
           ,[TipoDonacion]
           ,[Foto]
           ,[IdUsuarioCreador]
           ,[Estado]
           ,[Valoracion])
     VALUES
           ('Comedor nueva esperanza'
           ,'El comedor cuenta con 200 comensales por noche y no contamos con los recursos suficientes'
           ,getdate()
           ,getdate()+10
           ,'11556456789'
           ,1
           ,''
           ,1
           ,1
           ,90.00)
   END
END

-------------------------------------------------------------------------------