import {
  Component,
  input
} from '@angular/core';

import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-ticket-activity',
  imports: [
    MatCardModule,
    MatIconModule
  ],
  templateUrl: './ticket-activity.html',
  styleUrl: './ticket-activity.scss',
})
export class TicketActivityComponent {

  readonly ticketId = input.required<string>();
}