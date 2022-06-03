import { Component, OnInit } from '@angular/core';
import { MovieService } from '../../services/movie.service';
import IMovie from 'src/app/interface/IMovie';
import IOrder from 'src/app/interface/IOrder';
import { OrderService } from '../../services/order.service';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css'],
})
export class BookingComponent implements OnInit {
  constructor(private movieService: MovieService) {}
  movieList: IMovie[] = [];

  ngOnInit(): void {
    this.movieService.getMoviesFrontPage().subscribe((data) => {
      this.movieList = data;
      console.log(this.movieList);
    });
    console.log(this.movieList);
  }
}
