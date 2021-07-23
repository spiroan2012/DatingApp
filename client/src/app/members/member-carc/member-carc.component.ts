import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';

@Component({
  selector: 'app-member-carc',
  templateUrl: './member-carc.component.html',
  styleUrls: ['./member-carc.component.css']
})
export class MemberCarcComponent implements OnInit {
  @Input() member: Member;

  constructor() { 
    //this.member = {} as Member;
  }

  ngOnInit(): void {
  }

}
