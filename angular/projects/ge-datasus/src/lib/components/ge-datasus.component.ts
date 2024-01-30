import { Component, OnInit } from '@angular/core';
import { GeDatasusService } from '../services/ge-datasus.service';

@Component({
  selector: 'lib-ge-datasus',
  template: ` <p>ge-datasus works!</p> `,
  styles: [],
})
export class GeDatasusComponent implements OnInit {
  constructor(private service: GeDatasusService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
