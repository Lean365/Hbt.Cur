import {
  mask,
  maskPhone,
  maskIdCard,
  maskBankCard,
  maskEmail,
  maskName,
  maskAddress,
  maskPassword,
  maskCustom,
  maskObject,
  maskArray
} from './mask';

// 基本使用示例
console.log('手机号脱敏:', maskPhone('13812345678')); // 输出: 138****5678
console.log('身份证脱敏:', maskIdCard('110101199001011234')); // 输出: 110101********1234
console.log('银行卡脱敏:', maskBankCard('6222123412341234')); // 输出: 6222 **** **** 1234
console.log('邮箱脱敏:', maskEmail('test@example.com')); // 输出: t***@example.com
console.log('姓名脱敏:', maskName('张三')); // 输出: 张*
console.log('地址脱敏:', maskAddress('北京市海淀区中关村大街1号')); // 输出: 北京市海淀区****
console.log('密码脱敏:', maskPassword('123456')); // 输出: ******

// 使用通用脱敏方法
console.log('通用脱敏:', mask('13812345678', 'phone')); // 输出: 138****5678

// 自定义脱敏
console.log('自定义脱敏:', maskCustom('1234567890', 3, 3)); // 输出: 123****890

// 对象脱敏示例
const userInfo = {
  name: '张三',
  phone: '13812345678',
  idCard: '110101199001011234',
  email: 'zhangsan@example.com',
  address: '北京市海淀区中关村大街1号',
  password: '123456'
};

const maskRules = {
  name: 'name' as const,
  phone: 'phone' as const,
  idCard: 'idCard' as const,
  email: 'email' as const,
  address: 'address' as const,
  password: 'password' as const
};

console.log('对象脱敏:', maskObject(userInfo, maskRules));

// 数组对象脱敏示例
const userList = [
  {
    name: '张三',
    phone: '13812345678',
    idCard: '110101199001011234'
  },
  {
    name: '李四',
    phone: '13987654321',
    idCard: '110101199001011235'
  }
];

const listMaskRules = {
  name: 'name' as const,
  phone: 'phone' as const,
  idCard: 'idCard' as const
};

console.log('数组对象脱敏:', maskArray(userList, listMaskRules)); 