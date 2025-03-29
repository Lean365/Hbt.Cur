-- 修改用户角色表字段类型
ALTER TABLE hbt_id_user_role
ALTER COLUMN user_id bigint NOT NULL;

-- 添加索引
CREATE INDEX ix_user_role ON hbt_id_user_role(user_id, role_id); 