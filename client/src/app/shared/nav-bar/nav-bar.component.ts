import { Component, OnInit } from "@angular/core";
import { Observable, of } from "rxjs";
import { ResultResponse } from "src/app/_models/responses";
import { LoginUser, User } from "src/app/_models/user";
import { AccountService } from "src/app/_services/account.service";

@Component({
  selector: "app-nav-bar",
  templateUrl: "./nav-bar.component.html",
  styleUrls: ["./nav-bar.component.css"],
})
export class NavBarComponent implements OnInit {
  userModel?: User = new User();
  currentUser$: Observable<User | ResultResponse<User> | null> = of(null);
  formModel: LoginUser = new LoginUser();

  constructor(public accountService: AccountService) {}

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;

    const userString = localStorage.getItem("user");
    if (userString) {
      this.userModel = JSON.parse(userString) as User;
    }
  }

  login() {
    this.accountService.login(this.formModel).subscribe({
      next: (response) => {
        this.userModel = response?.result as User;
        console.log(this.userModel);
      },
      error: (error) => console.log(error),
    });
  }

  logOut() {
    this.accountService.logout();
  }
}
