import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { NavBarComponent } from "./shared/nav-bar/nav-bar.component";
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';

@NgModule({
  declarations: [AppComponent, NavBarComponent, HomeComponent, RegisterComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
