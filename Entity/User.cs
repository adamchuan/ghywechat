using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class User
    {
        private string username;
        private List<string> state;  
        private List<string> value;
        private List<string> state2;
        private DateTime exipre;
        private bool ondialog;
        /// <summary>
        /// 用户是否正在进行对话 
        /// </summary>
        public bool Ondialog
        {
            get { return ondialog; }
            set { ondialog = value; }
        }
        public DateTime Exipre
        {
            get { return exipre; }
            set { exipre = value; }
        }

        /// <summary>
        /// 2级状态 有什么用？ 比如1级状态为注册时候， 这个时候你要分成获取输入进入下一个状态和返回信息 让用户输入
        /// </summary>
        public List<string> State2
        {
            get { return state2; }
            set { state2 = value; }
        }
        /// <summary>
        /// 对应状态的值
        /// </summary>
        public List<string> Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        /// <summary>
        /// 一级状态
        /// </summary>
        public List<string> State
        {
            get { return state; }
            set { state = value; }
        }
        /// <summary>
        /// 用户ID，微信发过来时候已经加密了
        /// </summary>
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public User()
        {
            
        }
    }
}
