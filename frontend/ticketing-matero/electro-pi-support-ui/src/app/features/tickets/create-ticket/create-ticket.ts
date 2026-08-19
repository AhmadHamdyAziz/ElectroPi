import { Component, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';

import { CreateTicketRequest, TicketPriority } from '../../../shared/models/Ticket.models'; 
import { TicketService } from '../../../shared/services/TicketService/ticket.service';

@Component({
  selector: 'app-create-ticket',
  imports: [
    RouterLink,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatIconModule
  ],
  templateUrl: './create-ticket.html',
  styleUrl: './create-ticket.scss',
})

export class TicketCreate {

  private readonly ticketService = inject(TicketService);
  private readonly router = inject(Router);

  readonly TicketPriority = TicketPriority;

  readonly title = signal('');
  readonly description = signal('');
  readonly priority = signal<TicketPriority>(TicketPriority.Medium);

  readonly saving = signal(false);

  submit(): void {
    console.log('Submitting ticket');
console.log('ticket data', {
      title: this.title(),
      description: this.description(),
      priority: this.priority()
    });
    if (!this.title().trim() || !this.description().trim()) {
      return;
    }

    const request: CreateTicketRequest = {
      title: this.title().trim(),
      description: this.description().trim(),
      priority: this.priority()
    };

    console.log('Creating ticket', request);
    this.saving.set(true);

    this.ticketService.createTicket(request).subscribe({
      next: ticketId => {
        this.saving.set(false);

        console.log(`Ticket created successfully with ID: ${ticketId}`);

        this.router.navigate([
          `/tickets/details/${ticketId}`
        ]);
      },

      error: error => {
        console.error('Failed to create ticket', error);
        this.saving.set(false);
      }
    });
  }
}