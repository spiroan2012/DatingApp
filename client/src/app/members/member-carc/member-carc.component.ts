import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { PresenceService } from 'src/app/_services/presence.service';

@Component({
  selector: 'app-member-carc',
  templateUrl: './member-carc.component.html',
  styleUrls: ['./member-carc.component.css']
})
export class MemberCarcComponent implements OnInit {
  @Input() member: Member;

  constructor(private memberService: MembersService, private toastr: ToastrService, public presence: PresenceService) { 
    //this.member = {} as Member;
  }

  ngOnInit(): void {
  }

  addLike(member: Member){
    this.memberService.addLike(member.username).subscribe((response) =>
      this.toastr.success(response.toString())
    );
  }

}
