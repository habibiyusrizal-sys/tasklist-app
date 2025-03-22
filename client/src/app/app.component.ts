import { Component, inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RouterOutlet } from '@angular/router';
import {MatButtonModule} from '@angular/material/button';
import { MatInput, MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatListModule } from '@angular/material/list';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';

export interface AddResponse {
  message: string;
  data: {
    taskName: string;
    taskDate: string;
  };
}

@Component({
  selector: 'app-root',
  standalone: true,
  providers: [provideNativeDateAdapter()],
  imports: [RouterOutlet, MatButtonModule, MatInputModule, FormsModule, MatDatepickerModule, MatListModule, CommonModule, MatIconModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  http = inject(HttpClient);
  title = 'TaskApp';
  tasks: any[] = [];
  newTask: string = '';
  selectedDate: string = '';
  successMessage: string= '';

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks() {
    this.http.get('https://localhost:5001/api/tasks').subscribe({
      next: (response: any) => {
        this.tasks = response.map((task: any) => ({
          ...task,
        }));
      },
      error: (error) => console.log(error)

    });
  }


  toggleCompleted(task: any) {
    task.completed = !task.completed;
    console.log(`Task "${task.taskName}" completed: ${task.completed}`);
  }

  deleteTask(taskId: number):void {
    console.log(taskId);

    this.http
      .delete(`https://localhost:5001/api/tasks/delete/${taskId}`, {
        responseType: 'text',
      })
      .subscribe({
        next: (response) => {
          this.successMessage = response;

          this.tasks = this.tasks.filter((task: any) => task.id !== taskId);

          setTimeout(() => this.successMessage = '', 3000);
        },
        error: (error) => console.error(error),
      });
  }

  addTask() {
    if (!this.newTask.trim() || !this.selectedDate) {
      alert('Task name and date are required');
      return;
    }

    const formattedDate = new Date(this.selectedDate).toISOString().split('T')[0];

    const newTaskData = {
      taskName: this.newTask,
      taskDate: formattedDate
    }

    this.http.post<AddResponse>('https://localhost:5001/api/tasks/add', newTaskData)
      .subscribe({
        next: (response) => {
          this.successMessage = response.message;

           this.loadTasks();

           this.newTask = '';
           this.selectedDate = '';

           setTimeout(() => this.successMessage = '', 3000);
        },

        error: (error) => console.error(error)
      })
  }
}
