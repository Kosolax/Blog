import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Article } from '../../objects/article';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})
export class ArticlesComponent implements OnInit {
  public articles: Article[] = [];

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.httpClient = httpClient;
    this.baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.httpClient.get<Article[]>(this.baseUrl + 'articles').subscribe(result => {
      this.articles = result;
    }, error => console.error(error.error));
  }
}
