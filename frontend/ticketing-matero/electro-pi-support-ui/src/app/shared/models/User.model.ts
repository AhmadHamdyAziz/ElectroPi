export type RoleName = 'Admin' | 'Agent' | 'Customer';

export interface User {
  id: string;
  email: string;
  roleName: RoleName;
  roleId: string;
  customerId?: string;
  customerName?: string;
  createdAt: string;
}

export interface CreateUserRequest {
  email: string;
  password: string;
  roleId: string;
  customerId?: string;
}

export interface Role {
    name: string;
    id: string;
}