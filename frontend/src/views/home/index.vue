<template>
  <div class="home-portal-container">
    <a-row :gutter="16" align="stretch" class="full-height-row">
      <!-- 左栏：公告通知、待办事项 -->
      <a-col :span="8" class="full-height-col">
        <div class="column-flex">
          <a-card :bordered="false" class="portal-card news-card flex-card">
            <a-tabs v-model:activeKey="activeNewsTab" size="large">
              <a-tab-pane v-for="tab in newsTabs" :key="tab.key" :tab="t(tab.label)">
                <div class="news-main no-image-news">
                  <div class="news-list-block" style="width:100%;">
                    <a-list :data-source="newsData[activeNewsTab].slice(0,5)" :pagination="false">
                      <template #renderItem="{ item, index }">
                        <a-list-item>
                          <span class="news-list-index">{{ index + 1 }}</span>
                          <span class="news-list-title">{{ item.title }}</span>
                          <span class="news-list-date">{{ item.date }}</span>
                        </a-list-item>
                      </template>
                    </a-list>
                    <div class="news-list-pagination">
                      <a-button size="small">{{ t('home.prev') }}</a-button>
                      <a-button size="small">{{ t('home.next') }}</a-button>
                    </div>
                  </div>
                </div>
              </a-tab-pane>
            </a-tabs>
          </a-card>
          <a-card :bordered="false" class="portal-card todo-card flex-card" style="margin-top:16px;">
            <a-tabs v-model:activeKey="activeTodoTab" size="large">
              <a-tab-pane v-for="tab in todoTabs" :key="tab.key" :tab="t(tab.label)">
                <a-list :data-source="todoList[tab.key].slice(0,5)" :pagination="false">
                  <template #renderItem="{ item }">
                    <a-list-item>
                      <span class="todo-title">{{ item.title }}</span>
                      <span class="todo-type">{{ item.type }}</span>
                      <span class="todo-date">{{ item.date }}</span>
                    </a-list-item>
                  </template>
                </a-list>
                <div class="todo-pagination">
                  <a-button size="small">{{ t('home.prev') }}</a-button>
                  <a-button size="small">{{ t('home.next') }}</a-button>
                </div>
              </a-tab-pane>
            </a-tabs>
          </a-card>
        </div>
      </a-col>
      <!-- 中栏：会议、文件 -->
      <a-col :span="8" class="full-height-col">
        <div class="column-flex">
          <a-card :bordered="false" class="portal-card meeting-card flex-card half-card">
            <a-tabs v-model:activeKey="activeMeetingTab" size="large">
              <a-tab-pane v-for="tab in meetingTabs" :key="tab.key" :tab="t(tab.label)">
                <div class="news-main no-image-news">
                  <div class="news-list-block" style="width:100%;">
                    <a-list :data-source="tab.key === 'meeting' ? meetingList.slice(0,5) : carList.slice(0,5)" :pagination="false">
                      <template #renderItem="{ item, index }">
                        <a-list-item>
                          <span class="news-list-index">{{ index + 1 }}</span>
                          <span class="news-list-title">{{ item.title }}</span>
                          <span class="news-list-date">{{ item.date }} {{ item.time }}</span>
                          <a-tag v-if="item.status" :color="item.statusColor">{{ t(item.status) }}</a-tag>
                        </a-list-item>
                      </template>
                    </a-list>
                    <div class="news-list-pagination">
                      <a-button size="small">{{ t('home.prev') }}</a-button>
                      <a-button size="small">{{ t('home.next') }}</a-button>
                    </div>
                  </div>
                </div>
              </a-tab-pane>
            </a-tabs>
          </a-card>
          <a-card :bordered="false" class="portal-card meeting-card flex-card half-card" style="margin-top:16px;">
            <a-tabs v-model:activeKey="activeFileTab" size="large">
              <a-tab-pane v-for="tab in fileTabs" :key="tab.key" :tab="t(tab.label)">
                <div class="news-main no-image-news">
                  <div class="news-list-block" style="width:100%;">
                    <a-list :data-source="fileList[tab.key].slice(0,5)" :pagination="false">
                      <template #renderItem="{ item }">
                        <a-list-item>
                          <span class="file-icon"><component :is="item.icon" /></span>
                          <span class="news-list-title">{{ item.title }}</span>
                          <span class="news-list-date">{{ item.date }}</span>
                        </a-list-item>
                      </template>
                    </a-list>
                    <div class="news-list-pagination">
                      <a-button size="small">{{ t('home.prev') }}</a-button>
                      <a-button size="small">{{ t('home.next') }}</a-button>
                    </div>
                  </div>
                </div>
              </a-tab-pane>
            </a-tabs>
          </a-card>
        </div>
      </a-col>
      <!-- 右栏：日历和日程管理 -->
      <a-col :span="8" class="full-height-col">
        <div class="column-flex">
          <div class="right-flex">
            <a-card :bordered="false" class="portal-card calendar-card flex-card half-card">
              <hbt-calendar />
            </a-card>
            <a-card :bordered="false" class="portal-card schedule-list-card flex-card half-card" style="margin-top:16px;">
              <a-tabs v-model:activeKey="activeScheduleTab" size="large">
                <a-tab-pane v-for="tab in scheduleTabs" :key="tab.key" :tab="t(tab.label)">
                  <div class="schedule-list">
                    <a-list :data-source="tab.key === 'daily' ? scheduleList.slice(0,5) : weeklyScheduleList.slice(0,5)" :pagination="false">
                      <template #renderItem="{ item }">
                        <a-list-item>
                          <span class="schedule-dot"></span>
                          <span class="schedule-content">{{ item.title }}</span>
                          <span class="schedule-date">{{ item.date }}</span>
                        </a-list-item>
                      </template>
                    </a-list>
                    <div class="news-list-pagination">
                      <a-button size="small">{{ t('home.prev') }}</a-button>
                      <a-button size="small">{{ t('home.next') }}</a-button>
                    </div>
                  </div>
                </a-tab-pane>
              </a-tabs>
            </a-card>
          </div>
        </div>
      </a-col>
    </a-row>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { FileTextOutlined, FileExcelOutlined, FileImageOutlined, FilePptOutlined, FileZipOutlined, FileWordOutlined, VideoCameraOutlined } from '@ant-design/icons-vue'

const { t } = useI18n()
const activeNewsTab = ref('company')
const newsTabs = [
  { key: 'company', label: t('home.newstab.company.title') },
  { key: 'notice', label: t('home.newstab.notice.title') },
  { key: 'rule', label: t('home.newstab.rule.title') }
]

const meetingTabs = [
  { key: 'meeting', label: t('home.meetingtab.meeting.title') },
  { key: 'car', label: t('home.meetingtab.car.title') }
]

const fileTabs = [
  { key: 'file', label: t('home.filetab.myFile.title') },
  { key: 'cloud', label: t('home.filetab.myCloud.title') }
]

const scheduleTabs = [
  { key: 'daily', label: t('home.scheduletab.dailySchedule.title') },
  { key: 'weekly', label: t('home.scheduletab.weeklySchedule.title') }
]

const newsData: { [key: string]: { title: string; date: string }[] } = {
  company: [
    { title: t('home.newstab.company.new1'), date: '2019-06-04' },
    { title: t('home.newstab.company.new2'), date: '2019-06-04' },
    { title: t('home.newstab.company.new3'), date: '2019-06-04' },
    { title: t('home.newstab.company.new4'), date: '2019-06-04' },
    { title: t('home.newstab.company.new5'), date: '2019-06-04' },
  ],
  notice: [
    { title: t('home.newstab.notice.notice1'), date: '2019-06-04' },
    { title: t('home.newstab.notice.notice2'), date: '2019-06-04' },
    { title: t('home.newstab.notice.notice3'), date: '2019-06-04' },
    { title: t('home.newstab.notice.notice4'), date: '2019-06-04' },
    { title: t('home.newstab.notice.notice5'), date: '2019-06-04' }
  ],
  rule: [
    { title: t('home.newstab.rule.rule1'), date: '2019-06-04' },
    { title: t('home.newstab.rule.rule2'), date: '2019-06-04' },
    { title: t('home.newstab.rule.rule3'), date: '2019-06-04' },
    { title: t('home.newstab.rule.rule4'), date: '2019-06-04' },
    { title: t('home.newstab.rule.rule5'), date: '2019-06-04' }
  ]
}
const activeTodoTab = ref('todo')
const todoTabs = [
  { key: 'todo', label: t('home.todotab.myTodo.title') },
  { key: 'wait', label: t('home.todotab.myWait.title') },
  { key: 'done', label: t('home.todotab.myDone.title') },
  { key: 'read', label: t('home.todotab.myRead.title') },
  { key: 'urge', label: t('home.todotab.myUrge.title') }
]
const todoList: { [key: string]: { title: string; type: string; date: string }[] } = {
  todo: [
    { title: t('home.todotab.myTodo.todo1'), type: t('home.todotab.todo.todoType1'), date: '2019-05-08' },
    { title: t('home.todotab.myTodo.todo2'), type: t('home.todotab.todo.todoType2'), date: '2019-05-08' },
    { title: t('home.todotab.myTodo.todo3'), type: t('home.todotab.todo.todoType1'), date: '2019-05-08' },
    { title: t('home.todotab.myTodo.todo4'), type: t('home.todotab.todo.todoType2'), date: '2019-05-08' },
    { title: t('home.todotab.myTodo.todo5'), type: t('home.todotab.todo.todoType1'), date: '2019-05-08' }
  ],
  wait: [
    { title: t('home.todotab.myWait.wait1'), type: t('home.todotab.todo.todoType2'), date: '2019-05-08' },
    { title: t('home.todotab.myWait.wait2'), type: t('home.todotab.todo.todoType1'), date: '2019-05-08' },
    { title: t('home.todotab.myWait.wait3'), type: t('home.todotab.todo.todoType2'), date: '2019-05-08' },
    { title: t('home.todotab.myWait.wait4'), type: t('home.todotab.todo.todoType1'), date: '2019-05-08' },
    { title: t('home.todotab.myWait.wait5'), type: t('home.todotab.todo.todoType2'), date: '2019-05-08' }
  ],
  done: [
    { title: t('home.todotab.myDone.done1'), type: t('home.todotab.todo.todoType2'), date: '2019-05-08' },
    { title: t('home.todotab.myDone.done2'), type: t('home.todotab.todo.todoType1'), date: '2019-05-08' },
    { title: t('home.todotab.myDone.done3'), type: t('home.todotab.todo.todoType2'), date: '2019-05-08' },
    { title: t('home.todotab.myDone.done4'), type: t('home.todotab.todo.todoType1'), date: '2019-05-08' },
    { title: t('home.todotab.myDone.done5'), type: t('home.todotab.todo.todoType2'), date: '2019-05-08' }
  ],
  read: [
    { title: t('home.todotab.myRead.read1'), type: t('home.todotab.todo.todoType2'), date: '2019-05-08' },
    { title: t('home.todotab.myRead.read2'), type: t('home.todotab.todo.todoType1'), date: '2019-05-08' },
    { title: t('home.todotab.myRead.read3'), type: t('home.todotab.todo.todoType2'), date: '2019-05-08' },
    { title: t('home.todotab.myRead.read4'), type: t('home.todotab.todo.todoType1'), date: '2019-05-08' },
    { title: t('home.todotab.myRead.read5'), type: t('home.todotab.todo.todoType2'), date: '2019-05-08' }
  ],
  urge: [
    { title: t('home.todotab.myUrge.urge1'), type: t('home.todotab.todo.todoType2'), date: '2019-05-08' }, 
    { title: t('home.todotab.myUrge.urge2'), type: t('home.todotab.todo.todoType1'), date: '2019-05-08' },
    { title: t('home.todotab.myUrge.urge3'), type: t('home.todotab.todo.todoType2'), date: '2019-05-08' },
    { title: t('home.todotab.myUrge.urge4'), type: t('home.todotab.todo.todoType1'), date: '2019-05-08' },
    { title: t('home.todotab.myUrge.urge5'), type: t('home.todotab.todo.todoType2'), date: '2019-05-08' }
  ]
}
const activeFileTab = ref('file')
const fileList: { [key: string]: { icon: any; title: string; date: string }[] } = {
  file: [
    { icon: FileTextOutlined, title: t('home.filetab.myFile.file1'), date: '2019-05-08' },
    { icon: FileExcelOutlined, title: t('home.filetab.myFile.file2'), date: '2019-05-08' },
    { icon: FileImageOutlined, title: t('home.filetab.myFile.file3'), date: '2019-05-08' },
    { icon: FileImageOutlined, title: t('home.filetab.myFile.file4'), date: '2019-05-08' },
    { icon: FileImageOutlined, title: t('home.filetab.myFile.file5'), date: '2019-05-08' },

  ],
  cloud: [
    { icon: FileImageOutlined, title: t('home.filetab.myCloud.cloud1'), date: '2019-05-08' },
    { icon: FileImageOutlined, title: t('home.filetab.myCloud.cloud2'), date: '2019-05-08' },
    { icon: FileImageOutlined, title: t('home.filetab.myCloud.cloud3'), date: '2019-05-08' },
    { icon: FileImageOutlined, title: t('home.filetab.myCloud.cloud4'), date: '2019-05-08' },
    { icon: FileImageOutlined, title: t('home.filetab.myCloud.cloud5'), date: '2019-05-08' }
  ]
}
const meetingList = [
  { title: t('home.meetingtab.meeting.meeting1'), date: '2019-06-03', time: '09:30-10:00', status: 'home.status.agree', statusColor: 'blue' },
  { title: t('home.meetingtab.meeting.meeting2'), date: '2019-06-03', time: '09:30-10:00', status: 'home.status.wait', statusColor: 'green' },
  { title: t('home.meetingtab.meeting.meeting3'), date: '2019-06-03', time: '09:30-10:00', status: 'home.status.done', statusColor: 'gray' },
  { title: t('home.meetingtab.meeting.meeting4'), date: '2019-06-03', time: '09:30-10:00', status: 'home.status.done', statusColor: 'gray' },
  { title: t('home.meetingtab.meeting.meeting5'), date: '2019-06-03', time: '09:30-10:00', status: 'home.status.done', statusColor: 'gray' }
]
const scheduleList = [
  { title: t('home.scheduletab.dailySchedule.schedule1'), date: '2019-06-03 09:30-10:00' },
  { title: t('home.scheduletab.dailySchedule.schedule2'), date: '2019-06-03 09:30-10:00' }, 
  { title: t('home.scheduletab.dailySchedule.schedule3'), date: '2019-06-03 09:30-10:00' },
  { title: t('home.scheduletab.dailySchedule.schedule4'), date: '2019-06-03 09:30-10:00' },
  { title: t('home.scheduletab.dailySchedule.schedule5'), date: '2019-06-03 09:30-10:00' }
]
const activeScheduleTab = ref('daily')
const weeklyScheduleList = [
  { title: t('home.scheduletab.weeklySchedule.schedule1'), date: '2019-06-03 至 2019-06-09' },
  { title: t('home.scheduletab.weeklySchedule.schedule2'), date: '2019-06-03 至 2019-06-09' },
  { title: t('home.scheduletab.weeklySchedule.schedule3'), date: '2019-06-03 至 2019-06-09' },
  { title: t('home.scheduletab.weeklySchedule.schedule4'), date: '2019-06-03 至 2019-06-09' },
  { title: t('home.scheduletab.weeklySchedule.schedule5'), date: '2019-06-03 至 2019-06-09' }
]
const activeMeetingTab = ref('meeting')
const carList = [
  { title: t('home.meetingtab.car.car1'), date: '2019-06-03', time: '09:30-10:00', status: 'home.status.agree', statusColor: 'blue' },
  { title: t('home.meetingtab.car.car2'), date: '2019-06-03', time: '09:30-10:00', status: 'home.status.wait', statusColor: 'green' },
  { title: t('home.meetingtab.car.car3'), date: '2019-06-03', time: '09:30-10:00', status: 'home.status.done', statusColor: 'gray' },
  { title: t('home.meetingtab.car.car4'), date: '2019-06-03', time: '09:30-10:00', status: 'home.status.done', statusColor: 'gray' },
  { title: t('home.meetingtab.car.car5'), date: '2019-06-03', time: '09:30-10:00', status: 'home.status.done', statusColor: 'gray' }
]
</script>

<style lang="less" scoped>
.home-portal-container {
  padding: 0;
  background: var(--ant-color-bg-layout);
  height: calc(100vh - 148px - 48px - 40px);
}
.full-height-row {
  height: 100%;
}
.full-height-col {
  height: 100%;
}
.column-flex {
  display: flex;
  flex-direction: column;
  height: 100%;
}
.flex-card {
  flex: 1 1 0;
  min-height: 0;
  display: flex;
  flex-direction: column;
}
.half-card {
  flex: 1 1 0;
  min-height: 0;
}
.right-flex {
  display: flex;
  flex-direction: column;
  height: 100%;
}
.right-flex > .half-card {
  flex: 1 1 0;
  min-height: 0;
}
.portal-card {
  margin-bottom: 8px;
}
.news-main {
  display: flex;
  gap: 8px;
}
.news-image-block {
  width: 320px;
  flex-shrink: 0;
}
.news-image {
  width: 100%;
  height: 160px;
  object-fit: cover;
  border-radius: 8px;
}
.news-image-info {
  margin-top: 8px;
}
.news-image-title {
  font-weight: bold;
  font-size: 16px;
  margin-bottom: 4px;
}
.news-image-desc {
  color: #888;
  font-size: 13px;
  margin-bottom: 4px;
}
.news-image-tag {
  color: #f5222d;
  font-size: 12px;
  margin-right: 8px;
}
.news-image-date {
  color: #aaa;
  font-size: 12px;
}
.news-list-block {
  flex: 1;
}
.news-list-index {
  margin-right: 8px;
}
.news-list-title {
  font-weight: 500;
  margin-right: 8px;
  flex: 1;
}
.news-list-date {
  font-size: 12px;
  white-space: nowrap;
}
.news-list-pagination {
  margin-top: 8px;
  text-align: right;
}
.todo-title {
  font-weight: 500;
  margin-right: 8px;
  flex: 1;
}
.todo-type {
  margin-right: 8px;
  white-space: nowrap;
}
.todo-date {
  font-size: 12px;
  white-space: nowrap;
}
.todo-pagination {
  margin-top: 8px;
  text-align: right;
}
.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-weight: bold;
  margin-bottom: 8px;
}
.meeting-title {
  font-weight: 500;
  margin-right: 8px;
  flex: 1;
}
.meeting-date {
  margin-right: 8px;
  white-space: nowrap;
}
.meeting-time {
  margin-right: 8px;
  white-space: nowrap;
}
.schedule-list {
  padding-left: 8px;
}
.schedule-title {
  font-weight: bold;
  margin-bottom: 8px;
}
.schedule-dot {
  display: inline-block;
  width: 6px;
  height: 6px;
  border-radius: 50%;
  margin-right: 8px;
}
.schedule-content {
  margin-right: 8px;
  flex: 1;
}
.schedule-date {
  font-size: 12px;
  white-space: nowrap;
}
.file-icon {
  margin-right: 8px;
  font-size: 18px;
  display: flex;
  align-items: center;
}
.file-title {
  font-weight: 500;
  margin-right: 8px;
  flex: 1;
}
.file-date {
  font-size: 12px;
  white-space: nowrap;
}

.list-pagination {
  margin-top: 8px;
  text-align: right;
}

:deep(.ant-list-item) {
  display: flex;
  align-items: center;
  padding: 8px 0;
  border-bottom: 1px solid #f0f0f0;
}

:deep(.ant-list-item:last-child) {
  border-bottom: none;
}

:deep(.ant-list) {
  padding: 0 16px;
}

.week-view {
  padding: 8px;
}

.week-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
}

.week-date {
  font-weight: 500;
}

.week-grid {
  display: grid;
  grid-template-columns: repeat(7, 1fr);
  gap: 8px;
}

.week-item {
  border: 1px solid #f0f0f0;
  border-radius: 4px;
}

.week-item-header {
  padding: 8px;
  text-align: center;
  border-bottom: 1px solid #f0f0f0;
  font-weight: 500;
}

.week-item-content {
  padding: 8px;
  text-align: center;
  font-size: 12px;
}

.year-view {
  padding: 8px;
}

.year-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
}

.year-date {
  font-weight: 500;
}

.year-grid {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 16px;
}

.year-item {
  border: 1px solid #f0f0f0;
  border-radius: 4px;
}

.year-item-header {
  padding: 8px;
  text-align: center;
  border-bottom: 1px solid #f0f0f0;
  font-weight: 500;
}

.year-item-content {
  padding: 8px;
  text-align: center;
  font-size: 12px;
}

.calendar-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
}

.calendar-date {
  font-weight: 500;
}

.quarter-view {
  padding: 8px;
}

.quarter-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 16px;
}

.quarter-item {
  border: 1px solid #f0f0f0;
  border-radius: 4px;
}

.quarter-item-header {
  padding: 8px;
  text-align: center;
  border-bottom: 1px solid #f0f0f0;
  font-weight: 500;
}

.quarter-item-content {
  padding: 8px;
  text-align: center;
  font-size: 12px;
}

.calendar-grid {
  width: 100%;
}

.calendar-header-row {
  display: grid;
  grid-template-columns: repeat(7, 1fr);
  text-align: center;
  font-weight: 500;
  border-bottom: 1px solid #f0f0f0;
}

.calendar-header-cell {
  padding: 8px;
}

.calendar-body {
  margin-top: 8px;
}

.calendar-row {
  display: grid;
  grid-template-columns: repeat(7, 1fr);
  text-align: center;
}

.calendar-cell {
  padding: 8px;
  min-height: 32px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.calendar-date {
  font-size: 14px;
}

.current-month {
  font-weight: 500;
}

.calendar-cell:not(.current-month) {
  opacity: 0.5;
}

.year-content {
  padding: 8px;
}

.year-title {
  font-size: 16px;
  font-weight: 500;
  text-align: center;
  margin-bottom: 8px;
}

.year-quarters {
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 8px;
}

.quarter-item {
  border: 1px solid #f0f0f0;
  border-radius: 4px;
  padding: 8px;
}

.quarter-title {
  font-weight: 500;
  margin-bottom: 4px;
}

.quarter-date {
  font-size: 12px;
  color: #666;
}
</style> 