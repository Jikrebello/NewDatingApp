import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { IUser } from "./_models/user";
import { AccountService } from "./_services/account.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})
export class AppComponent implements OnInit {
  title = "Dating App";
  users: any;

  constructor(
    private https: HttpClient,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.setCurrentUser();
    this.getUsers();
  }

  getUsers() {
    this.https.get("https://localhost:5001/api/users/GetAllUsers").subscribe({
      next: (response) => (this.users = response),
      error: (error) => console.log(error),
      complete: () => console.log("Request has completed."),
    });
  }

  setCurrentUser() {
    const userString = localStorage.getItem("user");
    if (!userString) return;
    const user: IUser = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }
}
