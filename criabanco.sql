/*-----------------------------
Questionario Geral
------------------------------*/
insert into Questionarios values (getdate(),'GERAL',1)


insert into Perguntas values (getdate(),'Nome completo','4')
,(getdate(),'Sexo','2')
,(getdate(),'Nível de Escolaridade','2')
,(getdate(),'Data de nascimento','3')
,(getdate(),'Endereço','4')
,(getdate(),'Telefone','4')
,(getdate(),'Atividade que exerce','1')
,(getdate(), 'Se possui vínculpo empregatício, é permanente ou temporário?', '2')

insert into QuestionarioPerguntas values 
 (getdate(),'1', '3')
,(getdate(),'2', '3')
,(getdate(),'3', '3')
,(getdate(),'4', '3')
,(getdate(),'5', '3')
,(getdate(),'6', '3')
,(getdate(),'7', '3')



insert into Alternativas values
('Masculino', getdate(), '2')
,('Feminino', getdate(), '2')
,('Fundamental incompleto', getdate(), '3')
,('Fundamental completo', getdate(), '3')
,('Médio incompleto', getdate(), '3')
,('Médio completo', getdate(), '3')
,('Superior incompleto', getdate(), '3')
,('Superior completo', getdate(), '3')
,('Pós-graduação incompleta', getdate(), '3')
,('Pós-graduação completa', getdate(), '3')
,('Vínculo empregatício', getdate(), '7')
,('Aposentado', getdate(), '7')
,('Pensionista', getdate(), '7')
,('Afastado', getdate(), '7')
,('Outros', getdate(), '7')
,('Permanente', getdate(), '8')
,('Temporário', getdate(), '8')

/*-----------------------------
Questionario Comunidade Porto Seguro
------------------------------*/


insert into Questionarios values (getdate(),'Comunidade Porto Seguro',1)

insert into Perguntas values 
(getdate(),'Faz parte de alguma associação, entidade ou movimento na comunidade PS?','2')
,(getdate(),'Se sim, dê mais informações','4')
,(getdate(),'O que MAIS gosta na comunidade/bairro?','4')
,(getdate(),'O que MENOS gosta na comunidade/bairro?e','4')
,(getdate(),'Quais são as melhorias que gostaria de ver na comunidade?','4')

insert into QuestionarioPerguntas values 
 (getdate(),'9', '4')
,(getdate(),'10', '4')
,(getdate(),'11', '4')
,(getdate(),'12', '4')
,(getdate(),'13', '4')


insert into Alternativas values
('Sim', getdate(), '9')
,('Não', getdate(), '9')


/*
----------------------------------

insert into perguntas values 

select * from Perguntas 
update perguntas set TipoResposta = '2' where id = '7'

sp_help alternativas

insert into Alternativas

select * from Perguntas
select * from questionarios
select * from QuestionarioPerguntas

select p.id Numero_Pergunta,
	   p.Enunciado Enunciado,
	   a.Conteudo
from Questionarios q
inner join QuestionarioPerguntas qp
on q.id = qp.QuestionarioId
inner join Perguntas p
on qp.PerguntaId = p.id
inner join Alternativas A
on a.PerguntaId = p.id
where q.id = '3'*/

