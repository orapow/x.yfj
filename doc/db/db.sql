drop table if exists x_answer;

drop table if exists x_auth_login;

drop table if exists x_dict;

drop table if exists x_down;

drop table if exists x_fav;

drop table if exists x_option;

drop table if exists x_order;

drop table if exists x_paper;

drop table if exists x_question;

drop table if exists x_user;

/*==============================================================*/
/* Table: x_answer                                              */
/*==============================================================*/
create table x_answer
(
   answer_id            bigint not null,
   question_id          bigint,
   ques                 longblob,
   answer               longblob,
   parse                longblob,
   primary key (answer_id)
);

/*==============================================================*/
/* Table: x_auth_login                                          */
/*==============================================================*/
create table x_auth_login
(
   auth_login_id        bigint not null,
   user_id              bigint,
   appname              national varchar(64),
   openid               national varchar(64),
   token                national varchar(128),
   expire               datetime,
   ref_token            national varchar(128),
   primary key (auth_login_id)
);

/*==============================================================*/
/* Table: x_dict                                                */
/*==============================================================*/
create table x_dict
(
   dict_id              bigint not null auto_increment comment '�ֵ�',
   code                 national varchar(20),
   img                  national varchar(200),
   name                 national varchar(50) comment '����',
   jp                   national varchar(50) comment '��ƴ',
   upval                national varchar(10) comment '�ϼ�',
   value                national varchar(100) comment 'ֵ',
   sort                 int,
   f1                   int,
   f2                   int,
   f3                   national varchar(200),
   f4                   national varchar(200),
   primary key (dict_id)
);

alter table x_dict comment '�ֵ�';

/*==============================================================*/
/* Table: x_down                                                */
/*==============================================================*/
create table x_down
(
   down_id              bigint not null,
   user_id              bigint,
   paper_id             bigint,
   size                 int comment '1��A4
            2��A3
            3��B5
            4��B4',
   type                 int comment '1����ʦ��(�������)
            2��ѧ����(���ھ�β)',
   ctime                datetime,
   primary key (down_id)
);

/*==============================================================*/
/* Table: x_fav                                                 */
/*==============================================================*/
create table x_fav
(
   user_fav_id          bigint not null,
   user_id              bigint,
   type                 int comment '1������
            2���Ծ�',
   group                int,
   subject              int,
   content              longblob,
   ctime                datetime,
   primary key (user_fav_id)
);

/*==============================================================*/
/* Table: x_option                                              */
/*==============================================================*/
create table x_option
(
   option_id            bigint not null auto_increment,
   answer_id            bigint,
   content              longblob,
   primary key (option_id)
);

/*==============================================================*/
/* Table: x_order                                               */
/*==============================================================*/
create table x_order
(
   order_id             bigint not null auto_increment,
   user_id              bigint,
   etime                datetime,
   "desc"               national varchar(400),
   amount               decimal(18,2),
   no                   national varchar(64),
   wx_no                national varchar(64),
   ctime                datetime,
   status               int comment '״̬��
            1����֧��
            2����֧��
            3����ʧ��',
   err_msg              national varchar(400),
   primary key (order_id)
);

/*==============================================================*/
/* Table: x_paper                                               */
/*==============================================================*/
create table x_paper
(
   paper_id             bigint not null,
   user_id              bigint,
   tbook                int,
   subject              int,
   group                int,
   type                 int comment '1��ͬ������
            2����Ԫ�Ծ�
            3���¿��Ծ�
            4�����п���
            5����ĩ����
            6���п�ģ��
            7���п����
            8��ר���Ծ�
            9����ѧ����',
   area                 int,
   topic                national varchar(200),
   content              longblob,
   score                int,
   qcount               int,
   info                 longblob,
   ctime                datetime,
   primary key (paper_id)
);

/*==============================================================*/
/* Table: x_question                                            */
/*==============================================================*/
create table x_question
(
   question_id          bigint not null,
   tbook                int,
   group                int,
   subject              int,
   section              int,
   topic                int comment '1����ѡ
            2����ѡ��
            3�������
            4�����Ա��
            5���ʴ���
            6����������
            7������
            8���������Ķ�
            9���������Ķ�
            10��Ĭд
            11��ʫ�����
            12���ۺ���
            13����д
            14��д����',
   easy                 int comment '1������
            2����ͨ
            3������',
   knowledge            national varchar(800),
   title                longblob,
   ctime                datetime,
   src                  national varchar(200),
   mtime                datetime,
   hits                 int,
   primary key (question_id)
);

/*==============================================================*/
/* Table: x_user                                                */
/*==============================================================*/
create table x_user
(
   user_id              bigint not null,
   headimg              national varchar(200),
   uid                  national varchar(64),
   name                 national varchar(20),
   school               national varchar(60),
   tel                  national varchar(11),
   balance              decimal(18,2),
   ctime                datetime,
   etime                datetime,
   primary key (user_id)
);

alter table x_answer add constraint FK_Reference_6 foreign key (question_id)
      references x_question (question_id);

alter table x_auth_login add constraint FK_Reference_1 foreign key (user_id)
      references x_user (user_id);

alter table x_down add constraint FK_Reference_2 foreign key (user_id)
      references x_user (user_id);

alter table x_down add constraint FK_Reference_4 foreign key (paper_id)
      references x_paper (paper_id);

alter table x_fav add constraint FK_Reference_3 foreign key (user_id)
      references x_user (user_id);

alter table x_option add constraint FK_Reference_7 foreign key (answer_id)
      references x_answer (answer_id);

alter table x_order add constraint FK_Reference_5 foreign key (user_id)
      references x_user (user_id);
