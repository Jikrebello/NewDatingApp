import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, map } from "rxjs";
import { environment } from "src/environments/environment";
import { ResultResponse } from "../_models/responses";
import { ILoginUser, IRegisterUser, User } from "../_models/user";
import { UsersPaths } from "./api-endpoints";

@Injectable({
  providedIn: "root",
})
export class AccountService {
  private currentUserSource = new BehaviorSubject<
    User | ResultResponse<User> | null
  >(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}

  login(model: ILoginUser) {
    return this.http
      .post<ResultResponse<User>>(environment.baseUrl + UsersPaths.LOGIN, model)
      .pipe(
        map((response: ResultResponse<User>) => {
          if (response && response.result) {
            localStorage.setItem("user", JSON.stringify(response.result));
            this.currentUserSource.next(response);
            return response;
          } else {
            return null;
          }
        })
      );
  }

  register(model: IRegisterUser) {
    return this.http
      .post<ResultResponse<User>>(
        environment.baseUrl + UsersPaths.REGISTER,
        model
      )
      .pipe(
        map((response: ResultResponse<User>) => {
          if (response && response.result) {
            localStorage.setItem("user", JSON.stringify(response.result));
            this.currentUserSource.next(response);
          }
        })
      );
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem("user");
    this.currentUserSource.next(null);
  }
}
