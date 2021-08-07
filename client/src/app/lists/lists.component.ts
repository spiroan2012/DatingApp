import { Component, OnInit } from '@angular/core';
import { LikesParams } from '../_models/likesParams';
import { Member } from '../_models/member';
import { Pagination } from '../_models/pagination';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  members: Partial<Member[]>;
  pagination: Pagination;
  likesParams: LikesParams;

  constructor(private memberService: MembersService) {
    this.likesParams = new LikesParams();
   }

  ngOnInit(): void {
    this.loadLikes();
  }

  loadLikes(){
    this.memberService.getLikes(this.likesParams).subscribe(response =>{
      this.members = response.result,
      this.pagination = response.pagination
    })
  }

  pageChanged(event: any){
    this.likesParams.pageNumber = event.page;
    this.loadLikes();
  }

}
