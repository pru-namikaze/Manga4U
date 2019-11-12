import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HeaderComponent } from './templates/header/header.component';
import { MainTemplateComponent } from './templates/main-template/main-template.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MainTemplateComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
       { path: '', component: MainTemplateComponent, pathMatch: 'full' },
      // { path: 'counter', component: CounterComponent },
      // { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
