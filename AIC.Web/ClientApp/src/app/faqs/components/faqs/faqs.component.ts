import { Component, OnInit } from '@angular/core';
import { FaqsService } from '../../services/faqs.service';

@Component({
  selector: 'app-faqs',
  templateUrl: './faqs.component.html',
  styleUrls: ['./faqs.component.css']
})
export class FaqsComponent implements OnInit {
  activeIndex1: number = 0;

  activeIndex2: number = 0;
  title = "FAQs";
  scrollableTabs: any[] = Array.from({ length: 50 }, (_, i) => ({ title: `Tab ${i + 1}`, content: `Tab ${i + 1} Content` }));
  faqs: any


  constructor(private faqsService: FaqsService) { }

  ngOnInit(): void {
    this.faqsService.ListFAQs({}).subscribe(data => {
      this.faqs = this.GroupFAQTopics(data.Items)
    
    });
  }

  GroupFAQTopics(data) {
    let faq = Object.entries(data.reduce((r, c) => (r[c.TopicTitle ?? ""] = [...r[c.TopicTitle ?? ""] || [], c], r), {}))
    return faq.reduce((r, c) => (r.children.push(
      { TopicTitle: c[0], children: c[1] }), r), { name: "TopicsArray", children: [] }).children
      
  }
  index: number = null;
  lastIndex = -1;

  activeTabDetails() {
    document.querySelector('.headerCont').classList.remove("stickyNav")
    setTimeout(() => {
      window.scroll(0, 0)
    }, 0)
  
    this.index = this.lastIndex--;
    setTimeout(() => {
      this.index = this.lastIndex--;
    },100)
   
  }
}
