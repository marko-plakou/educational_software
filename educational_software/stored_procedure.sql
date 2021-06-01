
create or replace procedure update_success_rate(user_email text)
language plpgsql    
as $$
begin

update user_quiz_stats
set success_rate=t3.new_success_rate 
from
(select quiz_id,(100/3)*sum(case when is_correct then 1 else 0 end) as new_success_rate
from user_answers where email=user_email
group by quiz_id ) as  t3
where email=user_email and user_quiz_stats.quiz_id=t3.quiz_id;

commit;
end;$$