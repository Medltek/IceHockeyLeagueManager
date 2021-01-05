--- 3.6. Vymena hrace----
CREATE OR REPLACE PROCEDURE
    VymenaHrace(p_hrac_id1 hrac.hrac_id%type, p_hrac_id2 hrac.hrac_id%type)
AS
    v_tym1 INT;
    v_tym2 INT;
    v_klub1 INT;
    v_klub2 INT;
    v_cena1 INT;
    v_cena2 INT;
    v_rozdil INT;
    v_rozdil1 INT;
    v_rozdil2 INT;
    v_sponzoring INT;
BEGIN
    Select tym_id into v_tym1 from hrac where hrac_id = p_hrac_id1;
    Select tym_id into v_tym2 from hrac where hrac_id = p_hrac_id2;
    Select klub_id into v_klub1 from tym where tym_id = v_tym1;
    Select klub_id into v_klub2 from tym where tym_id = v_tym2;
    Select cena into v_cena1 from hrac where hrac_id = p_hrac_id1;
    Select cena into v_cena2 from hrac where hrac_id = p_hrac_id2;

    V_rozdil1 := V_cena1 - v_cena2;
    V_rozdil2 := V_cena2 - v_cena1;
    
    IF v_cena1 > v_cena2 THEN  select sponzoring into v_sponzoring from klub where klub_id = v_klub2;
                                V_rozdil := v_rozdil1;
        IF v_sponzoring < v_rozdil
            THEN RAISE INVALID_NUMBER; 
        ELSE 
            UPDATE klub SET sponzoring = sponzoring + v_rozdil WHERE klub_id = v_klub1;
            UPDATE klub SET sponzoring = sponzoring - v_rozdil WHERE klub_id = v_klub2;
            UPDATE hrac SET tym_id = v_tym1 WHERE hrac_id = p_hrac_id2;
            UPDATE hrac SET tym_id = v_tym2 WHERE hrac_id = p_hrac_id1;
        END IF;
        
    ELSIF  v_cena1 < v_cena2 THEN select sponzoring into v_sponzoring from klub where klub_id = v_klub1;
                                    V_rozdil := v_rozdil2;
         IF v_sponzoring < v_rozdil
            THEN RAISE INVALID_NUMBER; 
        ELSE 
            UPDATE hrac SET tym_id = v_tym1 WHERE hrac_id = p_hrac_id2;
            UPDATE hrac SET tym_id = v_tym2 WHERE hrac_id = p_hrac_id1;
        END IF; 
        
    ELSE
        UPDATE hrac SET tym_id = v_tym1 WHERE hrac_id = p_hrac_id2;
        UPDATE hrac SET tym_id = v_tym2 WHERE hrac_id = p_hrac_id1;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
    ROLLBACK;
END;

--- 1.5. Pripsani financnich prostredku----
CREATE OR REPLACE FUNCTION PripsaniFinancnichProstredku(p_klub_id IN klub.klub_id%type)
RETURN NUMBER
AS
v_castka INT;
BEGIN
    select SUM(castka) into v_castka from sponzor where klub_id = p_klub_id;
   Update klub
   set sponzoring = sponzoring + v_castka
   where klub_id = p_klub_id;
   RETURN v_castka;
END PripsaniFinancnichProstredku;

--- 3.5. Koupe tymu----
CREATE OR REPLACE PROCEDURE
    KoupeTymu(p_tym_id tym.tym_id%type, p_klub_id klub.klub_id%type)
AS
    v_puvodni_klub klub.klub_id%type;
    v_celkovaCena INT;
    v_sponzoring klub.sponzoring%type;
BEGIN
    Select klub_id into v_puvodni_klub from tym where tym_id = p_tym_id;
    Select SUM(cena) into v_celkovaCena From hrac Where tym_id = p_tym_id;
    select sponzoring into v_sponzoring from klub where klub_id = p_klub_id;
    
    IF v_celkovaCena > v_sponzoring THEN
        RAISE INVALID_NUMBER;
    ELSE
        UPDATE tym SET klub_id = p_klub_id WHERE tym_id = p_tym_id;
        UPDATE klub SET sponzoring = sponzoring + v_celkovaCena WHERE klub_id = v_puvodni_klub;
        UPDATE klub SET sponzoring = sponzoring - v_celkovaCena WHERE klub_id = p_klub_id;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
    ROLLBACK;
END;
--- 2.6. Koupe hrace----
CREATE OR REPLACE PROCEDURE
    KoupeHrace(p_hrac_id hrac.hrac_id%type, p_tym_id tym.tym_id%type, p_klub_id klub.klub_id%type)
AS
    v_puvodni_klub klub.klub_id%type;
    v_Cena INT;
    v_sponzoring klub.sponzoring%type;
BEGIN
    select tym.klub_id into v_puvodni_klub from tym join hrac on tym.tym_id = hrac.tym_id where hrac.hrac_id = p_hrac_id;
    Select cena into v_cena from hrac where hrac_id = p_hrac_id;
    select sponzoring into v_sponzoring from klub where klub_id = p_klub_id;
    
    IF v_Cena > v_sponzoring THEN
        RAISE INVALID_NUMBER;
    ELSE
        UPDATE hrac SET tym_id = p_tym_id WHERE hrac_id = p_hrac_id;
        UPDATE klub SET sponzoring = sponzoring + v_Cena WHERE klub_id = v_puvodni_klub;
        UPDATE klub SET sponzoring = sponzoring - v_Cena WHERE klub_id = p_klub_id;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
    ROLLBACK;
END;



-----3.7. Ocen?ní nejlepších hrá?? v lize mladších dvaceti let--------
CREATE OR REPLACE PROCEDURE Ocenit
AS
BEGIN
FOR c IN (SELECT hrac_id FROM (SELECT h.hrac_id, h.cena, FLOOR(MONTHS_BETWEEN(sysdate, datum_narozeni) / 12)  FROM hrac h 	
JOIN statistika s on h.hrac_id = s.hrac_id
WHERE FLOOR(MONTHS_BETWEEN(sysdate, datum_narozeni) / 12) < 20
ORDER BY body DESC) WHERE rownum <= 5)
        LOOP       
        UPDATE hrac SET cena = cena * 1.10 WHERE hrac_id = c.hrac_id;
        END LOOP;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
    ROLLBACK;
END;
