-- Generated by Oracle SQL Developer Data Modeler 19.1.0.081.0911
--   at:        2020-04-25 11:22:10 CEST
--   site:      Oracle Database 11g
--   type:      Oracle Database 11g

drop table  HRAC                                                                                                                             cascade constraints;
drop table  KLUB                                                                                                                             cascade constraints;
drop table  SPONZOR                                                                                                                          cascade constraints;
drop table  STATISTIKA                                                                                                                       cascade constraints;
drop table  TYM                                                                                                                              cascade constraints;
drop table  TYMY_ZAPASY                                                                                                                      cascade constraints;
drop table  ZAPAS                                                                                                                            cascade constraints;
drop table  ZEBRICEK                                                                                                                         cascade constraints;

DROP SEQUENCE klub_id_seq; 
DROP SEQUENCE tym_id_seq; 
DROP SEQUENCE sponzor_id_seq; 
DROP SEQUENCE zapas_id_seq; 
DROP SEQUENCE stat_id_seq; 
DROP SEQUENCE zebricek_id_seq; 
DROP SEQUENCE hrac_id_seq;

CREATE TABLE hrac (
    hrac_id          INTEGER NOT NULL,
    hrac_jmeno       VARCHAR2(30) NOT NULL,
    datum_narozeni   TIMESTAMP NOT NULL,
    cena             INTEGER NOT NULL,
    vyska            INTEGER NOT NULL,
    vaha             INTEGER NOT NULL,
    zahyb            VARCHAR2(30) NOT NULL,
    post             VARCHAR2(30) NOT NULL,
    tym_id           INTEGER NOT NULL,
    smazano          CHAR(1) NOT NULL 
);
/

ALTER TABLE hrac
    ADD CONSTRAINT hrac_pk PRIMARY KEY ( hrac_id );

CREATE SEQUENCE hrac_id_seq START WITH 1 NOCACHE ORDER;
CREATE OR REPLACE TRIGGER hrac_id_trg BEFORE
    INSERT ON hrac
    FOR EACH ROW
    WHEN ( new.hrac_id IS NULL )
BEGIN
    :new.hrac_id := hrac_id_seq.nextval;
END;
/


CREATE TABLE klub (
    klub_id      INTEGER NOT NULL,
    klub_nazev   VARCHAR2(30) NOT NULL,
    sponzoring   INTEGER NOT NULL,
    smazano      CHAR(1) NOT NULL
);
/
ALTER TABLE klub ADD CONSTRAINT klub_pk PRIMARY KEY ( klub_id );

CREATE SEQUENCE klub_id_seq START WITH 1 NOCACHE ORDER;
CREATE OR REPLACE TRIGGER klub_id_trg BEFORE
    INSERT ON klub
    FOR EACH ROW
    WHEN ( new.klub_id IS NULL )
BEGIN
    :new.klub_id := klub_id_seq.nextval;
END;
/

CREATE TABLE sponzor (
    sponzor_id      INTEGER NOT NULL,
    sponzor_nazev   VARCHAR2(30) NOT NULL,
    castka          INTEGER NOT NULL,
    klub_id    INTEGER NOT NULL,
    smazano         CHAR(1) NOT NULL
);
/
ALTER TABLE sponzor ADD CONSTRAINT sponzor_pk PRIMARY KEY ( sponzor_id );

CREATE SEQUENCE sponzor_id_seq START WITH 1 NOCACHE ORDER;
CREATE OR REPLACE TRIGGER sponzor_id_trg BEFORE
    INSERT ON sponzor
    FOR EACH ROW
    WHEN ( new.sponzor_id IS NULL )
BEGIN
    :new.sponzor_id := sponzor_id_seq.nextval;
END;
/

CREATE TABLE statistika (
    stat_id                 INTEGER NOT NULL,
    body                    INTEGER NOT NULL,
    goly                    INTEGER NOT NULL,
    asistence               INTEGER NOT NULL,
    zapasy                  INTEGER NOT NULL,
    hrac_id                 INTEGER NOT NULL,
    smazano                 CHAR(1) NOT NULL
    
);
/

ALTER TABLE statistika ADD CONSTRAINT statistika_pk PRIMARY KEY ( stat_id );


CREATE SEQUENCE stat_id_seq START WITH 1 NOCACHE ORDER;
CREATE OR REPLACE TRIGGER stat_id_trg BEFORE
    INSERT ON statistika
    FOR EACH ROW
    WHEN ( new.stat_id IS NULL )
BEGIN
    :new.stat_id := stat_id_seq.nextval;
END;
/

CREATE TABLE tym (
    tym_id                 INTEGER NOT NULL,
    tym_nazev              VARCHAR2(30) NOT NULL,
    klub_id           INTEGER NOT NULL,
    smazano                CHAR(1) NOT NULL
);
/


ALTER TABLE tym ADD CONSTRAINT tym_pk PRIMARY KEY ( 
                                                    tym_id );
CREATE SEQUENCE tym_id_seq START WITH 1 NOCACHE ORDER;
CREATE OR REPLACE TRIGGER tym_id_trg BEFORE
    INSERT ON tym
    FOR EACH ROW
    WHEN ( new.tym_id IS NULL )
BEGIN
    :new.tym_id := tym_id_seq.nextval;
END;
/

CREATE TABLE tymy_zapasy (
    tym_id          INTEGER NOT NULL,
    zapas_id        INTEGER NOT NULL
);

ALTER TABLE tymy_zapasy
    ADD CONSTRAINT tymy_zapasy_pk PRIMARY KEY ( tym_id,
                                                
                                                zapas_id
                                                 );

CREATE TABLE zapas (
    zapas_id                   INTEGER NOT NULL,
    goly_tym1                  INTEGER NOT NULL,
    goly_tym2                  INTEGER NOT NULL,
    datum                      TIMESTAMP NOT NULL,
    vyherce_id                 INTEGER,
    vyherce_v_prodlouzeni_id   INTEGER,
    smazano                    CHAR(1) NOT NULL
);

ALTER TABLE zapas ADD CONSTRAINT zapas_pk PRIMARY KEY ( zapas_id );

CREATE SEQUENCE zapas_id_seq START WITH 1 NOCACHE ORDER;
CREATE OR REPLACE TRIGGER zapas_id_trg BEFORE
    INSERT ON zapas
    FOR EACH ROW
    WHEN ( new.zapas_id IS NULL )
BEGIN
    :new.zapas_id := zapas_id_seq.nextval;
END;
/

CREATE TABLE zebricek (
    zebricek_id   INTEGER NOT NULL,
    zapasy        INTEGER NOT NULL,
    body          INTEGER NOT NULL,
    skore         INTEGER NOT NULL,
    tym_id        INTEGER NOT NULL,
    smazano       CHAR(1) NOT NULL
);

ALTER TABLE zebricek ADD CONSTRAINT zebricek_pk PRIMARY KEY ( zebricek_id );

CREATE SEQUENCE zebricek_id_seq START WITH 1 NOCACHE ORDER;
CREATE OR REPLACE TRIGGER zebricek_id_trg BEFORE
    INSERT ON zebricek
    FOR EACH ROW
    WHEN ( new.zebricek_id IS NULL )
BEGIN
    :new.zebricek_id := zebricek_id_seq.nextval;
END;
/


ALTER TABLE hrac
    ADD CONSTRAINT hrac_tym_fk FOREIGN KEY (
                                             tym_id )
        REFERENCES tym ( 
                         tym_id );

ALTER TABLE sponzor
    ADD CONSTRAINT sponzor_klub_fk FOREIGN KEY ( klub_id )
        REFERENCES klub ( klub_id );

ALTER TABLE statistika
    ADD CONSTRAINT statistika_hrac_fk FOREIGN KEY (
                                                    hrac_id )
        REFERENCES hrac ( hrac_id
                           );

ALTER TABLE tym
    ADD CONSTRAINT tym_klub_fk FOREIGN KEY ( klub_id )
        REFERENCES klub ( klub_id );

ALTER TABLE zebricek
    ADD CONSTRAINT zebricek_tym_fk FOREIGN KEY ( tym_id )
        REFERENCES tym ( tym_id );

ALTER TABLE tymy_zapasy
    ADD CONSTRAINT tymy_zapasy_tym_fk FOREIGN KEY ( 
                                                    tym_id )
        REFERENCES tym ( 
                         tym_id );

ALTER TABLE tymy_zapasy
    ADD CONSTRAINT tymy_zapasy_zapas_fk FOREIGN KEY ( zapas_id )
        REFERENCES zapas ( zapas_id );
























-- Oracle SQL Developer Data Modeler Summary Report: 
-- 
-- CREATE TABLE                             8
-- CREATE INDEX                             3
-- ALTER TABLE                             15
-- CREATE VIEW                              0
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                           0
-- ALTER TRIGGER                            0
-- CREATE COLLECTION TYPE                   0
-- CREATE STRUCTURED TYPE                   0
-- CREATE STRUCTURED TYPE BODY              0
-- CREATE CLUSTER                           0
-- CREATE CONTEXT                           0
-- CREATE DATABASE                          0
-- CREATE DIMENSION                         0
-- CREATE DIRECTORY                         0
-- CREATE DISK GROUP                        0
-- CREATE ROLE                              0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE SEQUENCE                          0
-- CREATE MATERIALIZED VIEW                 0
-- CREATE MATERIALIZED VIEW LOG             0
-- CREATE SYNONYM                           0
-- CREATE TABLESPACE                        0
-- CREATE USER                              0
-- 
-- DROP TABLESPACE                          0
-- DROP DATABASE                            0
-- 
-- REDACTION POLICY                         0
-- 
-- ORDS DROP SCHEMA                         0
-- ORDS ENABLE SCHEMA                       0
-- ORDS ENABLE OBJECT                       0
-- 
-- ERRORS                                   0
-- WARNINGS                                 0
