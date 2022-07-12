import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { DetailArticleComponent } from './detail-article/detail-article.component';
import { ArticlesComponent } from './articles/articles.component';
import { NavigationComponent } from './navigation/navigation.component';
import { environment } from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DetailArticleComponent,
    ArticlesComponent,
    NavigationComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'articles', component: ArticlesComponent },
      { path: 'article', component: DetailArticleComponent },
      { path: 'article/:id', component: DetailArticleComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
