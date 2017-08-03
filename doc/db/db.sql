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
   dict_id              bigint not null auto_increment comment '字典',
   code                 national varchar(20),
   img                  national varchar(200),
   name                 national varchar(50) comment '名称',
   jp                   national varchar(50) comment '简拼',
   upval                national varchar(10) comment '上级',
   value                national varchar(100) comment '值',
   sort                 int,
   f1                   int,
   f2                   int,
   f3                   national varchar(200),
   f4                   national varchar(200),
   primary key (dict_id)
);

alter table x_dict comment '字典';

/*==============================================================*/
/* Table: x_down                                                */
/*==============================================================*/
create table x_down
(
   down_id              bigint not null,
   user_id              bigint,
   paper_id             bigint,
   size                 int comment '1、A4
            2、A3
            3、B5
            4、B4',
   type                 int comment '1、教师用(答案在题后)
            2、学生用(答案在卷尾)',
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
   type                 int comment '1、试题
            2、试卷',
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
   status               int comment '状态：
            1、待支付
            2、已支付
            3、已失败',
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
   type                 int comment '1、同步测试
            2、单元试卷
            3、月考试卷
            4、期中考试
            5、期末考试
            6、中考模拟
            7、中考真卷
            8、专题试卷
            9、开学考试',
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
   topic                int comment '1、单选
            2、多选题
            3、填空题
            4、语言表达
            5、问答题
            6、名著导读
            7、翻译
            8、文言文阅读
            9、现在文阅读
            10、默写
            11、诗歌鉴赏
            12、综合题
            13、书写
            14、写作题',
   easy                 int comment '1、容易
            2、普通
            3、困难',
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
