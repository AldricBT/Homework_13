﻿using Homework_12_notMVVM.Infrastructure.Commands;
using Homework_12_notMVVM.Model.Data;
using Homework_12_notMVVM.Model.Workers;
using Homework_12_notMVVM.ViewModels.Base;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Homework_12_notMVVM.ViewModels
{
    internal class AuthorizationWindowViewModel : ViewModel
    {
        #region Fields and properties

        #region SelectedWorker
        private string _selectedWorker;

        /// <summary>
        /// Выбранный работник. В виде текста textblock
        /// </summary>
        public string SelectedWorker
        {
            get => _selectedWorker;
            set => Set(ref _selectedWorker, value);
        }

        #endregion
        #endregion

        #region Commands

        #region AuthorizationCommand
        public ICommand AuthorizationCommand { get; set; } //здесь живет сама команда (это по сути обычное свойство, чтобы его можно было вызвать из хамл)

        private void OnAuthorizationCommandExecuted(object p) //логика команды
        {
            switch (SelectedWorker)
            {
                case "Менеджер":
                    StaticMainData.AuthorizedWorker = new Manager();
                    break;

                default:
                    StaticMainData.AuthorizedWorker = new Consultant();
                    break;
            }
            
            DataWindow dataWindow = new DataWindow();
            dataWindow.Show();
        }

        private bool CanAuthorizationCommandExecute(object p) => true; //если команда должна быть доступна всегда, то просто возвращаем true                
        #endregion

        #endregion

        //Метод-костыль, для закрытия окна и вызова команды авторизации
        public void LogIn()
        {
            OnAuthorizationCommandExecuted(null);
        }


        public void InitializeCommand()
        {
            AuthorizationCommand = new RelayCommand(OnAuthorizationCommandExecuted, CanAuthorizationCommandExecute);
        }
        public AuthorizationWindowViewModel()
        {            
            InitializeCommand();
        }
    }
}