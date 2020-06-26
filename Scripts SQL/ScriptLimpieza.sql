BEGIN
 delete [TP-20192C].[dbo].[Denuncias]
END
BEGIN
 delete [TP-20192C].[dbo].[DonacionesHorasTrabajo]
END
BEGIN
 delete [TP-20192C].[dbo].[DonacionesInsumos]
END
BEGIN
 delete [TP-20192C].[dbo].[DonacionesMonetarias]
END
BEGIN
 delete [TP-20192C].[dbo].[MotivoDenuncia]
END
BEGIN
 delete [TP-20192C].[dbo].[Propuestas]
END
BEGIN
 delete [TP-20192C].[dbo].[PropuestasDonacionesHorasTrabajo]
END
BEGIN
 delete [TP-20192C].[dbo].[PropuestasDonacionesInsumos]
END
BEGIN
 delete [TP-20192C].[dbo].[PropuestasDonacionesMonetarias]
END
BEGIN
 delete [TP-20192C].[dbo].[PropuestasReferencias]
END
BEGIN
 delete [TP-20192C].[dbo].[PropuestasValoraciones]
END
BEGIN
 delete [TP-20192C].[dbo].[Usuarios] where TipoUsuario != '1'
END