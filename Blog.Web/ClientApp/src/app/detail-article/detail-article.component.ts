import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Article } from '../../objects/article';

@Component({
  selector: 'app-detail-article',
  templateUrl: './detail-article.component.html',
  styleUrls: ['./detail-article.component.css']
})
export class DetailArticleComponent implements OnInit {
  article: Article = {} as Article;
  id: number = 0;

  private isCreating: boolean = true;

  constructor(private router: Router, private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.isCreating = true;
    this.id = 0;
    this.route.params.subscribe(params =>
    {
      if (params['id'])
      {
        this.id = +params['id'];
        this.isCreating = false;
        this.httpClient.get<Article>(this.baseUrl + 'articles/' + this.id).subscribe(result => {
          this.article = result;
        }, error => console.error(error.error));
      }
    });
  }

  onSubmit(form: any) {
    if (form.valid) {
      if (this.isCreating) {
        this.httpClient.post<Article>(this.baseUrl + 'articles', this.article).subscribe(result => {
          this.router.navigate(["/articles"]);
        }, error => console.error(error.error));
      }
      else {
        this.httpClient.put<Article>(this.baseUrl + 'articles/' + this.id, this.article).subscribe(result => {
          this.router.navigate(["/articles"]);
        }, error => console.error(error.error));
      }
    }
  }
}
