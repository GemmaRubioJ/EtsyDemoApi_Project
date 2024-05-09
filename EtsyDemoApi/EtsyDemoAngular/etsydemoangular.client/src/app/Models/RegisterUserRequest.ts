export interface RegisterUserRequest {
  email: string;
  username: string;
  password: string;
  name: {
    firstName: string;
    lastName: string;
  };
  address: {
    city: string;
    street: string;
    number: string;
    zipcode: string;
  };
  phone: string;
}
