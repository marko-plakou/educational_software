
create or replace function lesson_completition_func() returns trigger as $examp_table$
BEGIN 
INSERT INTO lesson_completition(email,lesson_id,is_unlocked,is_complete,num_of_visits) values(new.email,0,true,false,0);
for i in 1..9 loop
	INSERT INTO lesson_completition(email,lesson_id,is_unlocked,is_complete,num_of_visits) values(new.email,i,false,false,0);	
end loop;
return new;
end;
$examp_table$ language plpgsql;

create trigger register_trigger_lessons after insert on user_info for each row execute procedure lesson_completition_func();