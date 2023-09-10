import { v4 as uuidv4 } from "uuid";
import { GUID } from "./types";

export interface IUser {
  id: GUID;
  name: string;
  emailAddress: string;
  token: string;
}
export class User implements IUser {
  id: GUID = uuidv4();
  name!: string;
  emailAddress!: string;
  token!: string;
}

export interface IRegisterUser {
  emailAddress: string;
  name: string;
  password: string;
}
export class RegisterUser implements IRegisterUser {
  emailAddress!: string;
  name!: string;
  password!: string;
}

export interface ILoginUser {
  emailAddress: string;
  password: string;
}
export class LoginUser implements ILoginUser {
  emailAddress!: string;
  password!: string;
}
