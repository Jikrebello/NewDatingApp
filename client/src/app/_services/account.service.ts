import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, map } from "rxjs";
import { environment } from "src/environments/environment";
import { UsersEndpoints } from "../_endpoints/api-endpoints";
import { ResultResponse } from "../_models/responses";
import { LoginUser, RegisterUser, User } from "../_models/user";

@Injectable({
  providedIn: "root",
})
export class AccountService {
  private currentUserSource = new BehaviorSubject<
    User | ResultResponse<User> | null
  >(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private https: HttpClient) {}

  login(model: LoginUser) {
    return this.https
      .post<ResultResponse<User>>(
        environment.baseUrl + UsersEndpoints.LOGIN,
        model
      )
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

  register(model: RegisterUser) {
    return this.https
      .post<ResultResponse<User>>(
        environment.baseUrl + UsersEndpoints.REGISTER,
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
