export interface Customer {
  id: string;
  name: string;
  createdAt: string;
}

export interface CreateCustomerRequest {
  name: string;
}