import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PerformanceComputingService } from '../../services/performance-computing.service';

@Component({
  selector: 'app-hpc-project-details',
  templateUrl: './hpc-project-details.component.html',
  styleUrls: ['./hpc-project-details.component.css']
})
export class HpcProjectDetailsComponent implements OnInit {
  projectDetails;
  id;
  constructor(private _route: ActivatedRoute, private projectsService: PerformanceComputingService, private _router: Router) { }

  ngOnInit(): void {
    this._route.params.subscribe(res => {
      this.id = res?.['id']
      if (this.id)
        this.GetHPCProjectDetails()
    });
  }

  GetHPCProjectDetails() {
    this.projectsService.GetHPCProjectById(this.id).subscribe(data => {
      this.projectDetails = data;
    });
  }

}
