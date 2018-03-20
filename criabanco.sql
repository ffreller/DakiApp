-- -- /*-----------------------------
-- -- Questionario Geral
-- -- ------------------------------*/
-- insert into Questionarios values (getdate(),'GERAL')


-- insert into Perguntas values (getdate(),'Nome completo','1','4')
--  ,(getdate(),'Sexo','1','2')
--  ,(getdate(),'Nível de Escolaridade','1','2')
--  ,(getdate(),'Data de nascimento','1','3')
--  ,(getdate(),'Endereço','1','4')
--  ,(getdate(),'Telefone','1','4')
--  ,(getdate(),'Atividade que exerce','1','1')
--  ,(getdate(), 'Se possui vínculo empregatício, é permanente ou temporário?','1', '2')

--  insert into QuestionarioPerguntas values 
--   (getdate(),'1', '1')
--  ,(getdate(),'2', '1')
--  ,(getdate(),'3', '1')
--  ,(getdate(),'4', '1')
--  ,(getdate(),'5', '1')
--  ,(getdate(),'6', '1')
--  ,(getdate(),'7', '1')

--  select * from QuestionarioPerguntas




--  insert into Alternativas values
--  ('Masculino', getdate(), '2')
--  ,('Feminino', getdate(), '2')
--  ,('Fundamental incompleto', getdate(), '3')
--  ,('Fundamental completo', getdate(), '3')
--  ,('Médio incompleto', getdate(), '3')
--  ,('Médio completo', getdate(), '3')
--  ,('Superior incompleto', getdate(), '3')
--  ,('Superior completo', getdate(), '3')
--  ,('Pós-graduação incompleta', getdate(), '3')
--  ,('Pós-graduação completa', getdate(), '3')
--  ,('Vínculo empregatício', getdate(), '7')
--  ,('Aposentado', getdate(), '7')
--  ,('Pensionista', getdate(), '7')
--  ,('Afastado', getdate(), '7')
--  ,('Outros', getdate(), '7')
--  ,('Permanente', getdate(), '8')
--  ,('Temporário', getdate(), '8')

-- -- /*-----------------------------
-- -- Questionario Comunidade Porto Seguro
-- -- ------------------------------*/


--  insert into Questionarios values (getdate(),'Comunidade Porto Seguro')

--  insert into Perguntas values 
--  (getdate(),'Faz parte de alguma associação, entidade ou movimento na comunidade PS?','1','2')
--  ,(getdate(),'Se sim, dê mais informações','0','4')
--  ,(getdate(),'O que MAIS gosta na comunidade/bairro?','1','4')
--  ,(getdate(),'O que MENOS gosta na comunidade/bairro?e','1','4')
--  ,(getdate(),'Quais são as melhorias que gostaria de ver na comunidade?','1','4')

--  insert into QuestionarioPerguntas values 
--   (getdate(),'9', '2')
--  ,(getdate(),'10', '2')
--  ,(getdate(),'11', '2')
--  ,(getdate(),'12', '2')
--  ,(getdate(),'13', '2')


--  insert into Alternativas values
--  ('Sim', getdate(), '9')
--  ,('Não', getdate(), '9')

--insert into Permissoes values (getdate(), 'Admin'), (getdate(), 'Cliente')
--insert into Usuarios values (getdate(), 'f.freller@gmail.com', 'Fabio','senha')
-- insert into UsuarioPermissoes values (getdate(), '1', '1')



--dotnet ef migrations add RespostasOK --startup-project ../DakiApp.webapi/DakiApp.webapi.csproj
--dotnet ef database update --startup-project ../DakiApp.webapi/DakiApp.webapi.csproj



